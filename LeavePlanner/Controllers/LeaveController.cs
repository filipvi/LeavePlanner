using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.Exceptions;
using LeavePlanner.Models.Results;
using LeavePlanner.Models.ViewModels.Leave;
using LeavePlanner.Utilities.Extensions;
using LeavePlanner.Utilities.Hubs;
using LeavePlanner.Utilities.Logger;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Z.EntityFramework.Plus;

namespace LeavePlanner.Controllers
{
    [Authorize]
    public class LeaveController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<CalendarHub> _hubContext;
        private readonly IHolidayService _holidayService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaveController(IMapper mapper, IUnitOfWork unitOfWork, IHubContext<CalendarHub> hubContext, IHolidayService holidayService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _holidayService = holidayService;
            _userManager = userManager;
        }

        #region Leaves  

        [AuthorizeRoles(UserRoles.Admin, UserRoles.Employee)]
        public async Task<IActionResult> Leaves()
        {
            var viewModel = new IndexLeaveViewModel();

            if (User.IsInRole(UserRoles.Employee))
            {
                var id = _userManager.GetUserId(User);
                viewModel.EmployeeId = int.Parse(id);
            }
            await viewModel.PrepareSelectLists(_unitOfWork);
            viewModel.PrepareManagementActions(User);

            return View(viewModel);
        }


        [HttpPost]
        public async Task<JsonResult> GetLeaves(int? employeeId)
        {
            var viewModel = new IndexLeaveViewModel { EmployeeId = employeeId };

            try
            {
                await viewModel.PrepareRequests(_unitOfWork, _mapper);
                return Json(viewModel.Leaves);
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, null);
                return Json(BadRequest("Error fetching data"));
            }
        }


        [HttpPost]
        public async Task<JsonResult> GetHolidays()
        {
            var viewModel = new IndexLeaveViewModel();
            await viewModel.PrepareHolidays(_mapper, _holidayService);

            return Json(viewModel.Leaves);
        }

        #endregion Leaves

        #region Create 

        public async Task<IActionResult> GetCreateModal(int employeeId, string startDate, string endDate)
        {
            var viewModel = new CreateLeaveViewModel(_holidayService)
            {
                EmployeeId = employeeId,
                DateFrom = startDate,
                DateTo = endDate,
            };

            try
            {
                await viewModel.PrepareData(_unitOfWork, _mapper);
                await viewModel.PrepareSelectLists(_unitOfWork);
            }
            catch (Exception e)
            {
                viewModel.HasErrorOccured = true;

                if (e is EntityNotFoundException || e is CreateNotAllowedException)
                {
                    viewModel.ErrorMessage = e.Message;
                }
                else
                {
                    Log.ControllerLog(this, e, employeeId);
                    viewModel.ErrorMessage = "Error fetching data";
                }

            }

            return PartialView("_CreateLeaveModal", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveViewModel viewModel)
        {
            var result = new Result();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ModelNotValidException("Error validating data");
                }

                await viewModel.CheckData(_unitOfWork);
                viewModel.PrepareDataForSaving(_mapper);

                var audit = new Audit { CreatedBy = User.GetLoggedInUserName() };
                await _unitOfWork.LeaveRepository.CreateAsync(viewModel.Leave);
                await _unitOfWork.CompleteAsync(audit);
                await _hubContext.Clients.All.SendAsync("RefreshEvents");

                result.Message = "Leave request created successfully";
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;

                if (e is EntityNotFoundException || e is CreateNotAllowedException)
                {
                    result.Message = e.Message;
                }
                else
                {
                    Log.ControllerLog(this, e, viewModel.EmployeeId);
                    result.Message = "Error while saving data";
                }
            }

            return Json(result);
        }

        #endregion Create 

        #region Details

        [AuthorizeRoles(UserRoles.Admin, UserRoles.Employee)]
        public async Task<PartialViewResult> GetInfoModal(int id)
        {
            var viewModel = new DetailsLeaveViewModel
            {
                Id = id
            };

            try
            {
                await viewModel.PrepareData(_unitOfWork, _mapper);
                viewModel.PrepareManagementActions(User);
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
            }

            return PartialView("_LeaveInfoModal", viewModel);
        }

        #endregion Details

        #region Pending

        [AuthorizeRoles(UserRoles.Admin)]
        public async Task<JsonResult> Pending(int id)
        {
            var result = new Result();

            try
            {
                await _unitOfWork.LeaveRepository.PendingAsync(id);
                var audit = new Audit
                {
                    CreatedBy = User.GetLoggedInUserName()
                };
                await _unitOfWork.CompleteAsync(audit);
                await _hubContext.Clients.All.SendAsync("RefreshEvents");
                result.Message = "Approved successfully";
                result.Success = true;
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
                result.Message = "Error occured";
                result.Success = false;
            }

            return Json(result);
        }

        #endregion Pending

        #region Approve

        [AuthorizeRoles(UserRoles.Admin)]
        public async Task<JsonResult> Approve(int id)
        {
            var result = new Result();

            try
            {
                await _unitOfWork.LeaveRepository.ApproveAsync(id);
                var audit = new Audit { CreatedBy = User.GetLoggedInUserName() };
                await _unitOfWork.CompleteAsync(audit);
                await _hubContext.Clients.All.SendAsync("RefreshEvents");
                result.Message = "Approved successfully";
                result.Success = true;
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
                result.Message = "Error occured";
                result.Success = false;
            }

            return Json(result);
        }

        #endregion Approve

        #region Decline

        [AuthorizeRoles(UserRoles.Admin)]
        public async Task<JsonResult> Decline(int id)
        {
            var result = new Result();

            try
            {
                await _unitOfWork.LeaveRepository.DeclineAsync(id);
                var audit = new Audit { CreatedBy = User.GetLoggedInUserName() };
                await _unitOfWork.CompleteAsync(audit);
                await _hubContext.Clients.All.SendAsync("RefreshEvents");
                result.Message = "Declined successfully";
                result.Success = true;
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
                result.Message = "Error occured";
                result.Success = false;
            }

            return Json(result);
        }

        #endregion Decline

        #region Delete

        [AuthorizeRoles(UserRoles.Admin, UserRoles.Employee)]
        public async Task<JsonResult> Delete(int id)
        {
            var result = new Result();

            try
            {
                await _unitOfWork.LeaveRepository.DeleteAsync(id);
                var audit = new Audit { CreatedBy = User.GetLoggedInUserName() };
                await _unitOfWork.CompleteAsync(audit);
                await _hubContext.Clients.All.SendAsync("RefreshEvents");
                result.Message = "Deleted successfully";
                result.Success = true;
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
                result.Message = "Error occured";
                result.Success = false;
            }

            return Json(result);
        }

        #endregion Delete
    }
}

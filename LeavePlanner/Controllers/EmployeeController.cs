using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.Exceptions;
using LeavePlanner.Models.Results;
using LeavePlanner.Models.ViewModels.Employee;
using LeavePlanner.Utilities.Extensions;
using LeavePlanner.Utilities.Hubs;
using LeavePlanner.Utilities.Logger;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Z.EntityFramework.Plus;

namespace LeavePlanner.Controllers
{
    [AuthorizeRoles(UserRoles.Admin)]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<CalendarHub> _hubContext;
        private readonly IHolidayService _holidayService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork, IHubContext<CalendarHub> hubContext,
            IHolidayService holidayService, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _holidayService = holidayService;
            _userManager = userManager;
        }

        #region Employees

        public IActionResult Employees()
        {
            var viewModel = new IndexEmployeeViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> GetEmployees()
        {
            var viewModel = new IndexEmployeeViewModel();

            try
            {
                var form = await Request.ReadFormAsync();
                await viewModel.GetData(form, _unitOfWork, _mapper);

                var result = Json(new
                {
                    draw = Convert.ToInt32(viewModel.Draw),
                    recordsTotal = viewModel.TotalRecords,
                    recordsFiltered = viewModel.RecFilter,
                    data = viewModel.Employees
                });

                return result;
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, null);

                var result = Json(new
                {
                    draw = 0,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = 0,
                    error = "Error fetching data"
                });

                return result;
            }
        }


        #endregion Employees

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = new DetailsEmployeeViewModel
            {
                Id = id
            };

            try
            {
                await viewModel.PrepareData(_unitOfWork, _mapper);
                return View(viewModel);
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
                TempData["ErrorMsg"] = "Error fetching employee details";
                return RedirectToAction(nameof(Employees));
            }
        }

        #endregion Details

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = new EditEmployeeViewModel { Id = id };
            try
            {
                await viewModel.PrepareData(_unitOfWork, _mapper);
                await viewModel.PrepareSelectList(_unitOfWork);

                return View(viewModel);
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, id);
                TempData["ErrorMsg"] = "Error fetching employee edit data";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ModelNotValidException("Error validating data");
                }

                var audit = new Audit { CreatedBy = User.GetLoggedInUserName() };

                await _unitOfWork.EmployeeRepository.EditAsync(viewModel);
                await _unitOfWork.CompleteAsync(audit);

                TempData["SuccessMsg"] = "Employee updated successfully";
                return RedirectToAction(nameof(Details), new { id = viewModel.Id });
            }
            catch (Exception e)
            {
                if (e is ModelNotValidException)
                {
                    TempData["ErrorMsg"] = e.Message;
                    await viewModel.PrepareData(_unitOfWork, _mapper);
                    await viewModel.PrepareSelectList(_unitOfWork);
                    return View(viewModel);
                }
                else
                {
                    Log.ControllerLog(this, e, viewModel.Id);
                    TempData["ErrorMsg"] = "Error updating employee";
                    return RedirectToAction(nameof(Details), new { id = viewModel.Id });
                }
            }
        }

        #endregion Edit

        #region Delete

        public async Task<JsonResult> Delete(int id)
        {
            var result = new Result();

            try
            {
                var audit = new Audit { CreatedBy = User.GetLoggedInUserName() };

                await _unitOfWork.EmployeeRepository.DeleteAsync(id, _userManager);
                await _unitOfWork.CompleteAsync(audit);

                result.Success = true;
                TempData["SuccessMsg"] = "Employee and related data deleted successfully";
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = "Error deleting employee";
                Log.ControllerLog(this, e, id);
            }

            return Json(result);
        }

        #endregion Delete

        #region Search

        public async Task<IActionResult> Search(string searchTerm, string returnUrl)
        {
            var viewModel = new SearchViewModel
            {
                SearchTerm = searchTerm
            };
            try
            {
                await viewModel.GetEmployee(_unitOfWork);
                TempData["SuccessMsg"] = "Employee fetched successfully";
                return RedirectToAction("Details", "Employee", new { id = viewModel.EmployeeId });
            }
            catch (Exception e)
            {
                if (e is EntityNotFoundException || e is ModelNotValidException)
                {
                    TempData["ErrorMsg"] = e.Message;
                }
                else
                {
                    Log.ControllerLog(this, e, null);
                    TempData["ErrorMsg"] = "No employees found!";
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("LandPage", "Home");
                }
            }
        }

        #endregion Search

    }
}

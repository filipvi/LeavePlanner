using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Models;
using LeavePlanner.Models.Exceptions;
using LeavePlanner.Models.ViewModels;
using LeavePlanner.Utilities.Extensions;
using LeavePlanner.Utilities.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LeavePlanner.Controllers
{
    public class TestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Startup page
        public IActionResult LandPage()
        {
            var viewModel = new LandPageViewModel();
            viewModel.PrepareHelpData();

            return View(viewModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Datatables()
        {
            return View();
        }

        public IActionResult DatatablesSearch()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
        {
            try
            {
                var viewModel = new TestViewModel();
                await viewModel.PrepareInitialData(_unitOfWork, _mapper);

                return View(viewModel);
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, null);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            var viewModel = new CreateViewModel();

            try
            {
                viewModel.PrepareDataAsync(_unitOfWork);
                return View(viewModel);
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, null);
                var errorModel = new ErrorViewModel
                {
                    Url = Request.GetDisplayUrl(),
                    Message = "Greška prilikom dohvaćanja podataka",
                    UserName = User.GetLoggedInUserDomainName()
                };

                return View("Error", errorModel);
            }
        }


        [HttpPost]
        public IActionResult Create(CreateViewModel createViewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelNotValidException("Greška prilikom validacije poslanih podataka");
            }

            try
            {
                _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                Log.ControllerLog(this, e, null);
            }

            return View(createViewModel);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using ReadingWithPassion.Web.Models.Repository.Interfaces;
using ReadingWithPassion.Web.ViewModels.Employee;

namespace ReadingWithPassion.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_unitOfWork.employeeRepository.GetAllToViewModels());
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            var employee = _unitOfWork.employeeRepository.FindToViewModel(id);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("~/Shared/_CustomError.cshtml");
            }
            return View(employee);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);

            var employee = _unitOfWork.employeeRepository.Add(new EmployeeViewModel
            {
                Name = employeeViewModel.Name,
                Email = employeeViewModel.Email,
                Department = employeeViewModel.Department,
                PhotoPath = ProccessUploadedFile(employeeViewModel)
            });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Details", new { Controller = "Employee", id = employee.Id });
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var employee = _unitOfWork.employeeRepository.Find(id);
            var employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
                return View(employeeViewModel);

            var employee = _unitOfWork.employeeRepository.FindToViewModel(employeeViewModel.Id);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("~/Shared/_CustomError.cshtml");
            }

            employee.Name = employeeViewModel.Name;
            employee.Email = employee.Email;
            employee.Department = employeeViewModel.Department;
            employee.PhotoPath = employeeViewModel.Photo == null ? employee.PhotoPath : ProccessUploadedFile(employeeViewModel);

            employee = _unitOfWork.employeeRepository.Update(employee);
            _unitOfWork.SaveChanges();
            if (employeeViewModel.ExistingPhotoPath != null)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/Employees", employeeViewModel.ExistingPhotoPath);
                System.IO.File.Delete(filePath);
            }
            return RedirectToAction("Index", new { Controller = "Home" });
        }

        private string ProccessUploadedFile(EmployeeCreateViewModel employeeViewModel)
        {
            var uploadedFoleder = Path.Combine(_hostingEnvironment.WebRootPath, @"images/Employees");
            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{employeeViewModel.Photo.FileName}";
            var filePath = Path.Combine(uploadedFoleder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                employeeViewModel.Photo.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
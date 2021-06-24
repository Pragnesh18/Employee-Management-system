using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employeemanagement.Models;
using Emp.Models;
using Emp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Employeemanagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployee _employeerepository;

        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(IEmployee employeerepository,IHostingEnvironment hostingEnvironment)
        {
            _employeerepository = employeerepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeerepository.GetAllEmployees();
            return View(model); 
        
        }

        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            var employee = _employeerepository.GetEmployee(id.Value);
            if(employee == null)
                {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
                }

            var homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeerepository.GetEmployee(id ?? 1),
                pagetitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult edit(int id)
        {
            Employee employee = _employeerepository.GetEmployee(id);
            EmployeeEditViewModel employeeeditviewmodel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.photopath
            };
            return View(employeeeditviewmodel);
        }

        [HttpPost]
        public IActionResult edit(EmployeeEditViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var Employee = _employeerepository.GetEmployee(employee.Id);
                Employee.Name = employee.Name;
                Employee.Email = employee.Email;
                Employee.Department = employee.Department;

                if (employee.photo != null)
                {
                    if(employee.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", employee.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Employee.photopath = ProcessFile(employee);
                }
                _employeerepository.Update(Employee);

                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessFile(EmployeeCreateViewModel employee)
        {
            string uniqueFilename = null;
            if (employee.photo != null)
            {
                string uploadsfolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFilename = Guid.NewGuid().ToString() + "_" + employee.photo.FileName;
                string filepath = Path.Combine(uploadsfolder, uniqueFilename);
                using var filestream = new FileStream(filepath, FileMode.Create);
                employee.photo.CopyTo(filestream);
            }
            return uniqueFilename;
        }

        [HttpPost]
        public IActionResult Create( EmployeeCreateViewModel employee)
        {
            if (ModelState.IsValid)
            {

                string uniqueFilename = ProcessFile(employee);

                Employee newemployee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Department = employee.Department,
                    photopath = uniqueFilename
                };
                _employeerepository.Add(newemployee);

                return RedirectToAction("details", new { id = newemployee.Id });
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
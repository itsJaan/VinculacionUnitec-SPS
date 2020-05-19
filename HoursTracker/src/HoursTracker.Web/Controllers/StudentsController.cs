using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Students;
using HoursTracker.Domain.Aggregates.Campuses;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Domain.Shared;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _studentService.All())
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    Account = student.Account,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    FirstSurname = student.FirstSurname,
                    SecondSurname = student.SecondSurname,
                    Settlement = student.Settlement.ToString(),
                    CareerName = student.CareerName,
                    CampusName = student.CampusName
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _studentService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(StudentViewModel studentViewModel)
        {
            var campusTmp = new Campus();
            var studCar = new StudentCareer();
            var careerTmp = new Career();
            var list = new List<StudentCareer>();
            careerTmp.Name = studentViewModel.CareerName;
            campusTmp.Name = studentViewModel.CampusName;
            studCar.Career = careerTmp;
            list.Add(studCar);
            var student = new Student
            {
                Id = studentViewModel.Id,
                Account = studentViewModel.Account,
                FirstName = studentViewModel.FirstName,
                SecondName = studentViewModel.SecondName,
                FirstSurname = studentViewModel.FirstSurname,
                SecondSurname = studentViewModel.SecondSurname,
                Campus = campusTmp,
                StudentCareers = list,
                Settlement = int.Parse(studentViewModel.Settlement)
            };

            await _studentService.Create(student);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _studentService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, StudentViewModel studentViewModel)
        {
            var student = new Student
            {
                Id = studentViewModel.Id,
                Account = studentViewModel.Account,
                FirstName = studentViewModel.FirstName,
                SecondName = studentViewModel.SecondName,
                FirstSurname = studentViewModel.FirstSurname,
                SecondSurname = studentViewModel.SecondSurname,
                //MajorCode = studentViewModel.MajorCode,
                //CampusCode = studentViewModel.CampusCode,
                Settlement = int.Parse(studentViewModel.Settlement)
            };
            await _studentService.Update(id, student);
        }
    }
}
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
                .Select(student => new SingleStudentViewModel
                {
                    Id = student.Id,
                    Account = student.Account,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    FirstSurname = student.FirstSurname,
                    SecondSurname = student.SecondSurname,
                    Settlement = student.Settlement,
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
        public async Task Create(CreateStudentViewModel studentViewModel)
        {
            var student = new CreateStudentDto
            {
                Account = studentViewModel.Account,
                FirstName = studentViewModel.FirstName,
                SecondName = studentViewModel.SecondName,
                FirstSurname = studentViewModel.FirstSurname,
                SecondSurname = studentViewModel.SecondSurname,
                Campus = studentViewModel.Campus,
                Careers = studentViewModel.Careers,
                Settlement = studentViewModel.Settlement
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
        public async Task Edit(int id, CreateStudentViewModel studentViewModel)
        {
            var student = new UpdateSudentDto()
            {
                Account = studentViewModel.Account,
                FirstName = studentViewModel.FirstName,
                SecondName = studentViewModel.SecondName,
                FirstSurname = studentViewModel.FirstSurname,
                SecondSurname = studentViewModel.SecondSurname,
                Campus = studentViewModel.Campus,
                Careers = studentViewModel.Careers,
                Settlement = studentViewModel.Settlement
            };
            await _studentService.Update(id, student);
        }
    }
}
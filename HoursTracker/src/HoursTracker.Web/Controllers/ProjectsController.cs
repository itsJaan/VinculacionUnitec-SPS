﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HoursTracker.Core.Projects;
using HoursTracker.Domain.Aggregates.Projects;
using HoursTracker.Web.Models;

namespace HoursTracker.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _projectService.All())
                .Select(project => new ProjectViewModel
                {
                    Id = project.Id,
                    Name = project.Name,
                    Code = project.Code,
                    Budget = project.Budget,
                    VinculationId = project.VinculationTypeId,
                    VinculationType = project.VinculationType.Type
        }) ;

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _projectService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(ProjectViewModel projectViewModel)
        {
            var project = new CreateProjectDto
            {
                Name = projectViewModel.Name,
                Code = projectViewModel.Code,
                Budget = projectViewModel.Budget,
                VinculationTypeId = projectViewModel.VinculationId
            };

            await _projectService.Create(project);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _projectService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, ProjectViewModel projectViewModel)
        {
            var project = new Project
            {
                Name = projectViewModel.Name,
                Code = projectViewModel.Code,
                Budget = projectViewModel.Budget,
                VinculationTypeId = projectViewModel.VinculationId

            };
            await _projectService.Update(id, project);
        }

    }
}

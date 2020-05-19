﻿using HoursTracker.Domain.Aggregates.Faculties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoursTracker.Core.Faculties
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;
        public FacultyService(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<IEnumerable<Faculty>> All()
        {
            return await _facultyRepository
                .Filter(faculty => !faculty.Disabled)
                .ToListAsync();
        }

        public async Task Create(Faculty faculty)
        {
            await _facultyRepository.Add(faculty);
        }

        public async Task<Faculty> FindById(int id)
        {
            return await _facultyRepository.FindById(id);      
        }

        public async Task Remove(int id)
        {
            var faculty = await _facultyRepository.FindById(id);
            await _facultyRepository.Disable(faculty);
        }

        public async Task Update(int id, Faculty faculty)
        {
            var existingFaculty = await _facultyRepository.FindById(id);

            existingFaculty.Code = faculty.Code;
            existingFaculty.Name = faculty.Name;

            await _facultyRepository.Update(existingFaculty);
        }
    }
}
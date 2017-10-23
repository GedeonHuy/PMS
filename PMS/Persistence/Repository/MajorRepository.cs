﻿using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;
using PMS.Persistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Persistence.Repository
{
    public class MajorRepository : IMajorRepository
    {
        private ApplicationDbContext context;

        public MajorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Major> GetMajor(int? id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Majors.FindAsync(id);
            }
            return await context.Majors
                .Include(m => m.Groups)
                .Include(m => m.Lecturers)
                .Include(m => m.Students)
                .Include(m => m.Projects)
                .SingleOrDefaultAsync(g => g.MajorId == id);
        }

        public void AddMajor(Major major)
        {
            context.Majors.Add(major);
        }

        public void RemoveMajor(Major major)
        {
            context.Remove(major);
        }

        public async Task<IEnumerable<Major>> GetMajors()
        {
            return await context.Majors
                .Include(m => m.Groups)
                .Include(m => m.Lecturers)
                .Include(m => m.Students)
                .Include(m => m.Projects)
                    .ToListAsync();
        }
    }
}
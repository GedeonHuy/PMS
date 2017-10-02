using AutoMapper;
using PMS.Models;
using PMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Student, StudentResource>()
            .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(v => v.Enrollments.Select(e => e.EnrollmentId)));

            //API Resource to domain
            CreateMap<StudentResource, Student>()
            .ForMember(s => s.Enrollments, opt => opt.MapFrom(sr => sr.Enrollments.Select(id => new Enrollment { EnrollmentId = id })));
        }
    }
}

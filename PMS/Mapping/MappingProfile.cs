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
            .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(v => v.Enrollments.Select(e => new EnrollmentResource { EnrollmentId = e.EnrollmentId })));

            CreateMap<ApplicationRole, RoleResource>();

            //API Resource to domain
            CreateMap<SaveStudentResource, Student>()
            .ForMember(s => s.StudentId, opt => opt.Ignore())
            .ForMember(s => s.Enrollments, opt => opt.Ignore())
            .AfterMap((sr, s) =>
            {
                //remove unselected enrollments
                var removedEnrollments = s.Enrollments.Where(e => !sr.Enrollments.Contains(e.EnrollmentId));
                foreach (var e in removedEnrollments)
                {
                    s.Enrollments.Remove(e);
                }

                //add new enrollments
                var addedEnrollments = sr.Enrollments.Where(id => !s.Enrollments.Any(e => e.EnrollmentId == id)).Select(id => new Enrollment { EnrollmentId = id });
                foreach (var e in addedEnrollments)
                {
                    s.Enrollments.Add(e);
                }
            });


            CreateMap<RoleResource, ApplicationRole>()
                .ForMember(r => r.Id, opt => opt.Ignore());
        }
    }
}

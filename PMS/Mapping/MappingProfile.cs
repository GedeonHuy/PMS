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
            CreateMap<Group, GroupResource>()
            .ForMember(gr => gr.Lecturer, opt => opt.MapFrom(g => new LecturerResource
            {
                LecturerId = g.LecturerId,
                Name = g.Lecturer.Name,
                Address = g.Lecturer.Address,
                DateOfBirth = g.Lecturer.DateOfBirth,
                Email = g.Lecturer.Email,
                IsDeleted = g.Lecturer.IsDeleted,
                PhoneNumber = g.Lecturer.PhoneNumber
            }));

            //API Resource to domain
            CreateMap<SaveStudentResource, Student>()
            .ForMember(s => s.Id, opt => opt.Ignore())
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
            CreateMap<EnrollmentResource, Enrollment>()
            .ForMember(e => e.EnrollmentId, opt => opt.Ignore());
            CreateMap<GroupResource, Group>()
            .ForMember(g => g.GroupId, opt => opt.Ignore());
        }
    }
}

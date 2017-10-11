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
            CreateMap<Council, CouncilResource>()
                .ForMember(cr => cr.CouncilEnrollments, opt => opt.MapFrom(c => c.CouncilEnrollments));

            CreateMap<CouncilEnrollment, CouncilEnrollmentResource>()
            .ForMember(cr => cr.Lecturer, opt => opt.MapFrom(c => new LecturerResource
            {
                LecturerId = c.Lecturer.LecturerId,
                Name = c.Lecturer.Name,
                Address = c.Lecturer.Address,
                DateOfBirth = c.Lecturer.DateOfBirth,
                Email = c.Lecturer.Email,
                IsDeleted = c.Lecturer.IsDeleted,
                PhoneNumber = c.Lecturer.PhoneNumber
            }))
            .ForMember(cr => cr.Council, opt => opt.MapFrom(c => new CouncilResource
            {
                CouncilId = c.Council.CouncilId,
                ResultGrade = c.Council.ResultGrade,
                ResultScore = c.Council.ResultScore,
                IsDeleted = c.Council.IsDeleted
            }));

            CreateMap<Grade, GradeResource>()
               .ForMember(gr => gr.Enrollment, opt => opt.MapFrom(g => g.Enrollment));

            CreateMap<Student, StudentResource>()
                .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(v => v.Enrollments.Select(e => new EnrollmentResource { EnrollmentId = e.EnrollmentId })));

            CreateMap<Enrollment, EnrollmentResource>()
                .ForMember(er => er.Student, opt => opt.MapFrom(e => e.Student));

            CreateMap<ApplicationRole, RoleResource>();

            CreateMap<Project, ProjectResource>()
                .ForMember(pr => pr.Groups, opt => opt.MapFrom(p => p.Groups));

            CreateMap<Lecturer, LecturerResource>()
                .ForMember(lr => lr.Groups, opt => opt.MapFrom(l => l.Groups))
                .ForMember(lr => lr.CouncilEnrollments, opt => opt.MapFrom(l => l.CouncilEnrollments));

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

            CreateMap<CouncilResource, Council>()
                .ForMember(c => c.CouncilId, opt => opt.Ignore());

            CreateMap<CouncilEnrollmentResource, CouncilEnrollment>()
                .ForMember(c => c.CouncilEnrollmentId, opt => opt.Ignore());

            CreateMap<GradeResource, Grade>()
                .ForMember(g => g.GradeId, opt => opt.Ignore());

            CreateMap<RoleResource, ApplicationRole>()
                .ForMember(r => r.Id, opt => opt.Ignore());

            CreateMap<EnrollmentResource, Enrollment>()
                .ForMember(e => e.EnrollmentId, opt => opt.Ignore());

            CreateMap<GroupResource, Group>()
                .ForMember(g => g.GroupId, opt => opt.Ignore());

            CreateMap<ProjectResource, Project>()
                .ForMember(p => p.ProjectId, opt => opt.Ignore());

            CreateMap<LecturerResource, Lecturer>()
                .ForMember(l => l.LecturerId, opt => opt.Ignore());
        }
    }
}

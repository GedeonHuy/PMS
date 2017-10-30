﻿using AutoMapper;
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
            .ForMember(cr => cr.GroupId, opt => opt.MapFrom(c => c.Group.GroupId))
            .ForMember(cr => cr.CouncilEnrollments, opt => opt.MapFrom(c => c.CouncilEnrollments.Select(cf => new CouncilEnrollment
            {
                CouncilEnrollmentId = cf.CouncilEnrollmentId,
                IsDeleted = cf.IsDeleted,
                Percentage = cf.Percentage

            })));

            CreateMap<Major, MajorResource>()
                .ForMember(gr => gr.Lecturers, opt => opt.MapFrom(g => g.Lecturers.Select(gf => new LecturerResource
                {
                    LecturerId = gf.LecturerId,
                    Name = gf.Name,
                    Address = gf.Address,
                    DateOfBirth = gf.DateOfBirth,
                    Email = gf.Email,
                    IsDeleted = gf.IsDeleted,
                    PhoneNumber = gf.PhoneNumber
                })))
                .ForMember(gr => gr.Students, opt => opt.MapFrom(g => g.Students.Select(gf => new StudentResource
                {
                    Email = gf.Email,
                    Address = gf.Address,
                    DateOfBirth = gf.DateOfBirth,
                    Id = gf.Id,
                    IsDeleted = gf.IsDeleted,
                    Name = gf.Name,
                    PhoneNumber = gf.PhoneNumber,
                    StudentCode = gf.StudentCode,
                    Year = gf.Year
                })))
                .ForMember(gr => gr.Groups, opt => opt.MapFrom(g => g.Groups.Select(gf => new GroupResource
                {
                    GroupId = gf.GroupId,
                    GroupName = gf.GroupName,
                    isDeleted = gf.isDeleted,
                    isConfirm = gf.isConfirm
                })))
                .ForMember(gr => gr.Projects, opt => opt.MapFrom(g => g.Projects.Select(gf => new ProjectResource
                {
                    ProjectId = gf.ProjectId,
                    Description = gf.Description,
                    ProjectCode = gf.ProjectCode,
                    Title = gf.Title,
                    Type = gf.Type
                })));

            CreateMap<Quarter, QuarterResource>()
                .ForMember(sr => sr.Groups, opt => opt.MapFrom(s => s.Groups.Select(sf => new GroupResource
                {
                    GroupId = sf.GroupId,
                    GroupName = sf.GroupName,
                    isDeleted = sf.isDeleted,
                    isConfirm = sf.isConfirm
                })))
                .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(s => s.Enrollments.Select(sf => new EnrollmentResource
                {
                    EnrollmentId = sf.EnrollmentId,
                    EndDate = sf.EndDate,
                    StartDate = sf.StartDate,
                    Type = sf.Type
                })));


            CreateMap<CouncilEnrollment, CouncilEnrollmentResource>()
                .ForMember(cr => cr.LecturerID, opt => opt.MapFrom(c => c.Lecturer.LecturerId))
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
                .ForMember(cr => cr.CouncilID, opt => opt.MapFrom(c => c.Council.CouncilId))
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
                .ForMember(sr => sr.MajorId, opt => opt.MapFrom(s => s.Major.MajorId))
                .ForMember(sr => sr.Major, opt => opt.MapFrom(s => new MajorResource
                {
                    MajorId = s.Major.MajorId,
                    MajorName = s.Major.MajorName,
                    MajorCode = s.Major.MajorCode
                }))
                .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(v => v.Enrollments.Select(e => new EnrollmentResource
                {
                    EnrollmentId = e.EnrollmentId,
                    EndDate = e.EndDate,
                    StartDate = e.StartDate,
                    Type = e.Type
                })));

            CreateMap<Enrollment, EnrollmentResource>()
                .ForMember(er => er.Grade, opt => opt.MapFrom(e => e.Grade))
                .ForMember(er => er.Student, opt => opt.MapFrom(e => e.Student))
                .ForMember(er => er.QuarterId, opt => opt.MapFrom(e => e.Quarter.QuarterId))
                .ForMember(er => er.Quarter, opt => opt.MapFrom(e => new Quarter
                {
                    QuarterId = e.Quarter.QuarterId,
                    QuarterName = e.Quarter.QuarterName,
                    QuarterEnd = e.Quarter.QuarterEnd,
                    QuarterStart = e.Quarter.QuarterStart
                }))
                .ForMember(er => er.GroupId, opt => opt.MapFrom(e => e.Group.GroupId))
                .ForMember(er => er.Group, opt => opt.MapFrom(e => new GroupResource
                {
                    GroupId = e.Group.GroupId,
                    GroupName = e.Group.GroupName,
                    isDeleted = e.Group.isDeleted,
                    isConfirm = e.Group.isConfirm
                }));

            CreateMap<ApplicationRole, RoleResource>();

            CreateMap<Project, ProjectResource>()
                .ForMember(pr => pr.MajorId, opt => opt.MapFrom(p => p.Major.MajorId))
                .ForMember(pr => pr.Major, opt => opt.MapFrom(p => new MajorResource
                {
                    MajorId = p.Major.MajorId,
                    MajorName = p.Major.MajorName,
                    MajorCode = p.Major.MajorCode
                }))
                .ForMember(pr => pr.Groups, opt => opt.MapFrom(p => p.Groups));

            CreateMap<Lecturer, LecturerResource>()
                .ForMember(lr => lr.MajorId, opt => opt.MapFrom(l => l.Major.MajorId))
                .ForMember(lr => lr.Major, opt => opt.MapFrom(l => new MajorResource
                {
                    MajorId = l.Major.MajorId,
                    MajorName = l.Major.MajorName,
                    MajorCode = l.Major.MajorCode
                }))
                .ForMember(lr => lr.Groups, opt => opt.MapFrom(l => l.Groups.Select(lf => new GroupResource
                {
                    GroupId = lf.GroupId,
                    GroupName = lf.GroupName,
                    isDeleted = lf.isDeleted,
                    isConfirm = lf.isConfirm
                })))
                .ForMember(lr => lr.CouncilEnrollments, opt => opt.MapFrom(l => l.CouncilEnrollments));

            CreateMap<Group, GroupResource>()
                .ForMember(er => er.QuarterId, opt => opt.MapFrom(e => e.Quarter.QuarterId))
                .ForMember(er => er.Quarter, opt => opt.MapFrom(e => new Quarter
                {
                    QuarterId = e.Quarter.QuarterId,
                    QuarterName = e.Quarter.QuarterName,
                    QuarterEnd = e.Quarter.QuarterEnd,
                    QuarterStart = e.Quarter.QuarterStart
                }))
                .ForMember(gr => gr.MajorId, opt => opt.MapFrom(g => g.Major.MajorId))
                .ForMember(gr => gr.Major, opt => opt.MapFrom(g => new MajorResource
                {
                    MajorId = g.Major.MajorId,
                    MajorName = g.Major.MajorName,
                    MajorCode = g.Major.MajorCode
                }))
                .ForMember(gr => gr.LecturerId, opt => opt.MapFrom(g => g.Lecturer.LecturerId))
                .ForMember(gr => gr.Lecturer, opt => opt.MapFrom(g => new LecturerResource
                {
                    LecturerId = g.Lecturer.LecturerId,
                    Name = g.Lecturer.Name,
                    Address = g.Lecturer.Address,
                    DateOfBirth = g.Lecturer.DateOfBirth,
                    Email = g.Lecturer.Email,
                    IsDeleted = g.Lecturer.IsDeleted,
                    PhoneNumber = g.Lecturer.PhoneNumber
                }))
                .ForMember(gr => gr.ProjectId, opt => opt.MapFrom(g => g.Project.ProjectId))
                .ForMember(gr => gr.Project, opt => opt.MapFrom(g => new ProjectResource
                {
                    ProjectId = g.Project.ProjectId,
                    Description = g.Project.Description,
                    ProjectCode = g.Project.ProjectCode,
                    Title = g.Project.Title,
                    Type = g.Project.Type
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

            CreateMap<MajorResource, Major>()
                .ForMember(m => m.MajorId, opt => opt.Ignore());

            CreateMap<QuarterResource, Quarter>()
                .ForMember(m => m.QuarterId, opt => opt.Ignore());
        }
    }
}

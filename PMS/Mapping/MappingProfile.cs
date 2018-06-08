using AutoMapper;
using PMS.Models;
using PMS.Models.TaskingModels;
using PMS.Resources;
using PMS.Resources.SubResources;
using PMS.Resources.TaskResources;
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
            CreateMap<Announcement, AnnouncementResource>()
                .ForMember(cr => cr.AnnouncementUsers, opt => opt.MapFrom(c => c.AnnouncementUsers.Select(cf => cf.AnnouncementUserId)));

            CreateMap<AnnouncementUser, AnnouncementUserResource>()
            .ForMember(cr => cr.AnnouncementId, opt => opt.MapFrom(c => c.Announcement.AnnouncementId))
            .ForMember(cr => cr.AppUser, opt => opt.MapFrom(c => new ApplicationUserResource
            {
                Avatar = c.AppUser.Avatar,
                CreatedOn = c.AppUser.CreatedOn,
                FullName = c.AppUser.FullName,
                IsDeleted = c.AppUser.IsDeleted,
                Major = c.AppUser.Major,
                UpdatedOn = c.AppUser.UpdatedOn,
                AnnouncementUsers = c.AppUser.AnnouncementUsers.Select(a => a.AnnouncementUserId).ToList()
            }));

            CreateMap<Board, BoardResource>()
            //.ForMember(cr => cr.Group, opt => opt.MapFrom(c => c.Group))
            .ForMember(cr => cr.ProjectName, opt => opt.MapFrom(c => c.Group.Project.Title))
            .ForMember(cr => cr.GroupName, opt => opt.MapFrom(c => c.Group.GroupName))
            .ForMember(cr => cr.GroupId, opt => opt.MapFrom(c => c.Group.GroupId))
            .ForMember(cr => cr.LecturerInformations, opt => opt.MapFrom(c => new LecturerInformationResource
            {
                Chair = new ChairResource
                {
                    LecturerId = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Chair").Lecturer.LecturerId,
                    ScorePercent = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Chair").Percentage,
                    Score = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Chair").Score
                },
                Secretary = new SecretaryResource
                {
                    LecturerId = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Secretary").Lecturer.LecturerId,
                    ScorePercent = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Secretary").Percentage,
                    Score = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Secretary").Score
                },
                Reviewer = new ReviewerResource
                {
                    LecturerId = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Reviewer").Lecturer.LecturerId,
                    ScorePercent = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Reviewer").Percentage,
                    Score = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Reviewer").Score
                },
                Supervisor = new SupervisorResource
                {
                    LecturerId = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Supervisor").Lecturer.LecturerId,
                    ScorePercent = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Supervisor").Percentage,
                    Score = c.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Supervisor").Score
                }
            }))
            .ForMember(cr => cr.BoardEnrollments, opt => opt.MapFrom(c => c.BoardEnrollments.Select(cf => cf.BoardEnrollmentId)));

            CreateMap<Major, MajorResource>()
                .ForMember(gr => gr.Lecturers, opt => opt.MapFrom(g => g.Lecturers.Select(gf => gf.LecturerId)))
                .ForMember(gr => gr.Students, opt => opt.MapFrom(g => g.Students.Select(gf => gf.Id)))
                .ForMember(gr => gr.Groups, opt => opt.MapFrom(g => g.Groups.Select(gf => gf.GroupId)))
                .ForMember(gr => gr.Projects, opt => opt.MapFrom(g => g.Projects.Select(gf => gf.ProjectId)));

            CreateMap<Quarter, QuarterResource>()
                .ForMember(sr => sr.Groups, opt => opt.MapFrom(s => s.Groups.Select(sf => sf.GroupId)))
                .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(s => s.Enrollments.Select(sf => sf.EnrollmentId)));


            CreateMap<BoardEnrollment, BoardEnrollmentResource>()
                .ForMember(cr => cr.LecturerID, opt => opt.MapFrom(c => c.Lecturer.LecturerId))
                .ForMember(cr => cr.Recommendations, opt => opt.MapFrom(c => c.Recommendations.Select(cf => cf.Description)))
                .ForMember(cr => cr.Lecturer, opt => opt.MapFrom(c => new LecturerResource
                {
                    LecturerId = c.Lecturer.LecturerId,
                    Name = c.Lecturer.Name,
                    Address = c.Lecturer.Address,
                    DateOfBirth = c.Lecturer.DateOfBirth,
                    Email = c.Lecturer.Email,
                    IsDeleted = c.Lecturer.IsDeleted,
                    PhoneNumber = c.Lecturer.PhoneNumber,
                    BoardEnrollments = c.Lecturer.BoardEnrollments.Select(cf => cf.BoardEnrollmentId).ToList(),
                    Groups = c.Lecturer.Groups.Select(cf => cf.GroupId).ToList(),
                    Projects = c.Lecturer.Projects.Select(cf => cf.ProjectId).ToList()

                }))
                .ForMember(cr => cr.BoardID, opt => opt.MapFrom(c => c.Board.BoardId))
                .ForMember(cr => cr.Board, opt => opt.MapFrom(c => new BoardResource
                {
                    BoardId = c.Board.BoardId,
                    ResultGrade = c.Board.ResultGrade,
                    ResultScore = c.Board.ResultScore,
                    IsDeleted = c.Board.IsDeleted,
                    isAllScored = c.Board.isAllScored,
                    DateTime = c.Board.DateTime,
                    Order = c.Board.Order,
                    BoardEnrollments = c.Board.BoardEnrollments.Select(cf => cf.BoardEnrollmentId).ToList()
                }));

            CreateMap<Grade, GradeResource>()
               .ForMember(gr => gr.Enrollment, opt => opt.MapFrom(g => g.Enrollment));

            CreateMap<Student, StudentResource>()
                .ForMember(sr => sr.MajorId, opt => opt.MapFrom(s => s.Major.MajorId))
                .ForMember(sr => sr.Major, opt => opt.MapFrom(s => new MajorResource
                {
                    MajorId = s.Major.MajorId,
                    MajorName = s.Major.MajorName,
                    MajorCode = s.Major.MajorCode,
                    isDeleted = s.Major.isDeleted,
                    Groups = s.Major.Groups.Select(sf => sf.GroupId).ToList(),
                    Students = s.Major.Students.Select(sf => sf.Id).ToList(),
                    Projects = s.Major.Projects.Select(sf => sf.ProjectId).ToList(),
                    Lecturers = s.Major.Lecturers.Select(sf => sf.LecturerId).ToList(),
                }))
                .ForMember(sr => sr.Enrollments, opt => opt.MapFrom(v => v.Enrollments.Select(e => e.EnrollmentId)));

            CreateMap<Enrollment, EnrollmentResource>()
                .ForMember(er => er.StudentEmail, opt => opt.MapFrom(e => e.Student.Email))
                .ForMember(er => er.StudentId, opt => opt.MapFrom(e => e.Student.Id))
                .ForMember(er => er.Grade, opt => opt.MapFrom(e => e.Grade))
                //.ForMember(er => er.Student, opt => opt.MapFrom(e => new StudentResource
                //{
                //    Id = e.Student.Id,
                //    Address = e.Student.Address,
                //    DateOfBirth = e.Student.DateOfBirth,
                //    Email = e.Student.Email,
                //    IsDeleted = e.Student.IsDeleted,
                //    Name = e.Student.Name,
                //    PhoneNumber = e.Student.PhoneNumber,
                //    Year = e.Student.Year,
                //    StudentCode = e.Student.StudentCode,
                //    Enrollments = e.Student.Enrollments.Select(ef => ef.EnrollmentId).ToList(),
                //    Major = new MajorResource
                //    {
                //        MajorId = e.Student.Major.MajorId,
                //        MajorCode = e.Student.Major.MajorCode,
                //        MajorName = e.Student.Major.MajorName,
                //        isDeleted = e.Student.Major.isDeleted,
                //        Groups = e.Student.Major.Groups.Select(sf => sf.GroupId).ToList(),
                //        Students = e.Student.Major.Students.Select(sf => sf.Id).ToList(),
                //        Projects = e.Student.Major.Projects.Select(sf => sf.ProjectId).ToList(),
                //        Lecturers = e.Student.Major.Lecturers.Select(sf => sf.LecturerId).ToList(),
                //    }
                //}))
                .ForMember(er => er.QuarterId, opt => opt.MapFrom(e => e.Quarter.QuarterId))
                .ForMember(er => er.Quarter, opt => opt.MapFrom(e => new Quarter
                {
                    QuarterId = e.Quarter.QuarterId,
                    QuarterName = e.Quarter.QuarterName,
                    QuarterEnd = e.Quarter.QuarterEnd,
                    QuarterStart = e.Quarter.QuarterStart,
                    isDeleted = e.Quarter.isDeleted
                }))
                .ForMember(er => er.GroupId, opt => opt.MapFrom(e => e.Group.GroupId))
                .ForMember(er => er.Group, opt => opt.MapFrom(e => new GroupResource
                {
                    GroupId = e.Group.GroupId,
                    GroupName = e.Group.GroupName,
                    LinkGitHub = e.Group.LinkGitHub,
                    isDeleted = e.Group.isDeleted,
                    isConfirm = e.Group.isConfirm,
                    ResultGrade = e.Group.ResultGrade,
                    ResultScore = e.Group.ResultScore,
                    Enrollments = e.Group.Enrollments.Select(ef => ef.EnrollmentId).ToList(),
                    UploadedFiles = e.Group.UploadedFiles.Select(ef => ef.UploadedFileId).ToList(),
                    Tasks = e.Group.Tasks.Select(ef => ef.TaskId).ToList(),
                    Comments = e.Group.Board.BoardEnrollments.Select(sf => sf.Comment).ToList(),
                    LecturerEmail = e.Group.Lecturer.Email,
                    StudentEmails = e.Group.Enrollments.Select(sf => sf.Student.Email).ToList(),
                    StudentInformations = e.Group.Enrollments.Select(sf => new StudentInformationResource
                    {
                        Email = sf.Student.Email,
                        Name = sf.Student.Name
                    }).ToList(),
                    UploadFilesInformation = e.Group.UploadedFiles.Select(sf => new UploadFilesInformationResource
                    {
                        UploadedFileId = sf.UploadedFileId,
                        Title = sf.Title,
                        Url = sf.Url
                    }).ToList(),
                }))
                .ForMember(er => er.LecturerId, opt => opt.MapFrom(e => e.Lecturer.LecturerId))
                .ForMember(er => er.Lecturer, opt => opt.MapFrom(e => new LecturerResource
                {
                    LecturerId = e.Lecturer.LecturerId,
                    Name = e.Lecturer.Name,
                    Address = e.Lecturer.Address,
                    DateOfBirth = e.Lecturer.DateOfBirth,
                    Email = e.Lecturer.Email,
                    IsDeleted = e.Lecturer.IsDeleted,
                    PhoneNumber = e.Lecturer.PhoneNumber,
                    BoardEnrollments = e.Lecturer.BoardEnrollments.Select(cf => cf.BoardEnrollmentId).ToList(),
                    Groups = e.Lecturer.Groups.Select(cf => cf.GroupId).ToList(),
                    Projects = e.Lecturer.Projects.Select(cf => cf.ProjectId).ToList()
                }));

            CreateMap<ApplicationRole, RoleResource>();

            CreateMap<Project, ProjectResource>()
                .ForMember(pr => pr.MajorId, opt => opt.MapFrom(p => p.Major.MajorId))
                .ForMember(pr => pr.Major, opt => opt.MapFrom(p => new MajorResource
                {
                    MajorId = p.Major.MajorId,
                    MajorName = p.Major.MajorName,
                    MajorCode = p.Major.MajorCode,
                    isDeleted = p.Major.isDeleted,
                    Groups = p.Major.Groups.Select(sf => sf.GroupId).ToList(),
                    Students = p.Major.Students.Select(sf => sf.Id).ToList(),
                    Projects = p.Major.Projects.Select(sf => sf.ProjectId).ToList(),
                    Lecturers = p.Major.Lecturers.Select(sf => sf.LecturerId).ToList(),
                }))
                .ForMember(pr => pr.LecturerId, opt => opt.MapFrom(p => p.Lecturer.LecturerId))
                .ForMember(pr => pr.Lecturer, opt => opt.MapFrom(p => new LecturerResource
                {
                    LecturerId = p.Lecturer.LecturerId,
                    Name = p.Lecturer.Name,
                    Address = p.Lecturer.Address,
                    DateOfBirth = p.Lecturer.DateOfBirth,
                    Email = p.Lecturer.Email,
                    IsDeleted = p.Lecturer.IsDeleted,
                    PhoneNumber = p.Lecturer.PhoneNumber,
                    BoardEnrollments = p.Lecturer.BoardEnrollments.Select(cf => cf.BoardEnrollmentId).ToList(),
                    Groups = p.Lecturer.Groups.Select(cf => cf.GroupId).ToList(),
                    Projects = p.Lecturer.Projects.Select(cf => cf.ProjectId).ToList()
                }))
                .ForMember(pr => pr.TagProjects, opt => opt.MapFrom(p => p.TagProjects.Select(pf => pf.TagProjectId)))
                .ForMember(pr => pr.Tags, opt => opt.MapFrom(p => p.TagProjects.Select(pf => pf.Tag.TagName)))
                .ForMember(pr => pr.Groups, opt => opt.MapFrom(p => p.Groups.Select(pf => pf.GroupId)))
                .ForMember(pr => pr.Categories, opt => opt.MapFrom(p => p.Categories.Select(pf => new CategoryInformationResource
                {
                    CategoryName = pf.CategoryName,
                    Confidence = pf.Confidence
                })));

            CreateMap<Lecturer, LecturerResource>()
                .ForMember(lr => lr.MajorId, opt => opt.MapFrom(l => l.Major.MajorId))
                .ForMember(lr => lr.Major, opt => opt.MapFrom(l => new MajorResource
                {
                    MajorId = l.Major.MajorId,
                    MajorName = l.Major.MajorName,
                    MajorCode = l.Major.MajorCode,
                    isDeleted = l.Major.isDeleted,
                    Groups = l.Major.Groups.Select(sf => sf.GroupId).ToList(),
                    Students = l.Major.Students.Select(sf => sf.Id).ToList(),
                    Projects = l.Major.Projects.Select(sf => sf.ProjectId).ToList(),
                    Lecturers = l.Major.Lecturers.Select(sf => sf.LecturerId).ToList(),
                }))
                .ForMember(lr => lr.Groups, opt => opt.MapFrom(l => l.Groups.Select(lf => lf.GroupId)))
                .ForMember(lr => lr.Projects, opt => opt.MapFrom(l => l.Projects.Select(lf => lf.ProjectId)))
                .ForMember(lr => lr.ProjectDetails, opt => opt.MapFrom(l => l.Projects))
                .ForMember(lr => lr.BoardEnrollments, opt => opt.MapFrom(l => l.BoardEnrollments.Select(lf => lf.BoardEnrollmentId)));

            CreateMap<Group, GroupResource>()
                .ForMember(er => er.BoardId, opt => opt.MapFrom(e => e.Board.BoardId))
                .ForMember(gr => gr.Comments, opt => opt.MapFrom(g => g.Board.BoardEnrollments.Select(gf => gf.Comment)))
                .ForMember(er => er.Board, opt => opt.MapFrom(e => new BoardResource
                {
                    BoardId = e.Board.BoardId,
                    GroupId = e.Board.Group.GroupId,
                    IsDeleted = e.Board.IsDeleted,
                    ResultGrade = e.Board.ResultGrade,
                    ResultScore = e.Board.ResultScore,
                    isAllScored = e.Board.isAllScored,
                    ProjectName = e.Board.Group.Project.Title,
                    DateTime = e.Board.DateTime,
                    Order = e.Board.Order,
                    GroupName = e.Board.Group.GroupName,
                    LecturerInformations = new LecturerInformationResource
                    {
                        Chair = new ChairResource
                        {
                            LecturerId = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Chair").Lecturer.LecturerId,
                            ScorePercent = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Chair").Percentage,
                            Score = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Chair").Score
                        },
                        Secretary = new SecretaryResource
                        {
                            LecturerId = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Secretary").Lecturer.LecturerId,
                            ScorePercent = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Secretary").Percentage,
                            Score = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Secretary").Score
                        },
                        Reviewer = new ReviewerResource
                        {
                            LecturerId = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Reviewer").Lecturer.LecturerId,
                            ScorePercent = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Reviewer").Percentage,
                            Score = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Reviewer").Score
                        },
                        Supervisor = new SupervisorResource
                        {
                            LecturerId = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Supervisor").Lecturer.LecturerId,
                            ScorePercent = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Supervisor").Percentage,
                            Score = e.Board.BoardEnrollments.FirstOrDefault(cf => cf.BoardRole.BoardRoleName == "Supervisor").Score
                        }
                    },
                    BoardEnrollments = e.Board.BoardEnrollments.Select(ef => ef.BoardEnrollmentId).ToList()
                }))
                .ForMember(gr => gr.Enrollments, opt => opt.MapFrom(g => g.Enrollments.Select(gf => gf.EnrollmentId)))
                .ForMember(gr => gr.StudentEmails, opt => opt.MapFrom(g => g.Enrollments.Select(gf => gf.Student.Email)))
                .ForMember(gr => gr.StudentInformations, opt => opt.MapFrom(g => g.Enrollments.Select(gf => new StudentInformationResource
                {
                    Name = gf.Student.Name,
                    Email = gf.Student.Email
                })))
                 .ForMember(gr => gr.UploadFilesInformation, opt => opt.MapFrom(g => g.UploadedFiles.Select(gf => new UploadFilesInformationResource
                 {
                     UploadedFileId = gf.UploadedFileId,
                     Title = gf.Title,
                     Url = gf.Url
                 })))
                .ForMember(gr => gr.UploadedFiles, opt => opt.MapFrom(g => g.UploadedFiles.Select(gf => gf.UploadedFileId)))
                .ForMember(gr => gr.Tasks, opt => opt.MapFrom(g => g.Tasks.Select(gf => gf.TaskId)))
                .ForMember(er => er.QuarterId, opt => opt.MapFrom(e => e.Quarter.QuarterId))
                .ForMember(er => er.Quarter, opt => opt.MapFrom(e => new QuarterResource
                {
                    QuarterId = e.Quarter.QuarterId,
                    QuarterName = e.Quarter.QuarterName,
                    QuarterEnd = e.Quarter.QuarterEnd,
                    QuarterStart = e.Quarter.QuarterStart,
                    isDeleted = e.Quarter.isDeleted,
                    Groups = e.Quarter.Groups.Select(gf => gf.GroupId).ToList(),
                    Enrollments = e.Quarter.Enrollments.Select(ef => ef.EnrollmentId).ToList()
                }))
                .ForMember(gr => gr.MajorId, opt => opt.MapFrom(g => g.Major.MajorId))
                .ForMember(gr => gr.Major, opt => opt.MapFrom(g => new MajorResource
                {
                    MajorId = g.Major.MajorId,
                    MajorName = g.Major.MajorName,
                    MajorCode = g.Major.MajorCode,
                    isDeleted = g.Major.isDeleted,
                    Groups = g.Major.Groups.Select(sf => sf.GroupId).ToList(),
                    Students = g.Major.Students.Select(sf => sf.Id).ToList(),
                    Projects = g.Major.Projects.Select(sf => sf.ProjectId).ToList(),
                    Lecturers = g.Major.Lecturers.Select(sf => sf.LecturerId).ToList(),
                }))
                .ForMember(gr => gr.LecturerId, opt => opt.MapFrom(g => g.Lecturer.LecturerId))
                .ForMember(gr => gr.LecturerEmail, opt => opt.MapFrom(g => g.Lecturer.Email))
                .ForMember(gr => gr.Lecturer, opt => opt.MapFrom(g => new LecturerResource
                {
                    LecturerId = g.Lecturer.LecturerId,
                    Name = g.Lecturer.Name,
                    Address = g.Lecturer.Address,
                    DateOfBirth = g.Lecturer.DateOfBirth,
                    Email = g.Lecturer.Email,
                    IsDeleted = g.Lecturer.IsDeleted,
                    PhoneNumber = g.Lecturer.PhoneNumber,
                    BoardEnrollments = g.Lecturer.BoardEnrollments.Select(cf => cf.BoardEnrollmentId).ToList(),
                    Groups = g.Lecturer.Groups.Select(cf => cf.GroupId).ToList(),
                    Projects = g.Lecturer.Projects.Select(cf => cf.ProjectId).ToList()
                }))
                .ForMember(gr => gr.ProjectId, opt => opt.MapFrom(g => g.Project.ProjectId))
                .ForMember(gr => gr.Project, opt => opt.MapFrom(g => new ProjectResource
                {
                    ProjectId = g.Project.ProjectId,
                    Description = g.Project.Description,
                    ProjectCode = g.Project.ProjectCode,
                    Title = g.Project.Title,
                    Type = g.Project.Type,
                    IsDeleted = g.Project.IsDeleted,
                    IsCompleted = g.Project.IsCompleted,
                    Groups = g.Project.Groups.Select(gf => gf.GroupId).ToList(),
                    Categories = g.Project.Categories.Select(gf => new CategoryInformationResource
                    {
                        CategoryName = gf.CategoryName,
                        Confidence = gf.Confidence
                    }).ToList()
                }));
            CreateMap<Excel, ExcelResource>();

            CreateMap<UploadedFile, UploadedFileResource>()
                .ForMember(cr => cr.TaskId, opt => opt.MapFrom(c => c.Task.TaskId))
                .ForMember(cr => cr.GroupId, opt => opt.MapFrom(c => c.Group.GroupId))
                .ForMember(sr => sr.Group, opt => opt.MapFrom(s => new GroupResource
                {
                    GroupId = s.Group.GroupId,
                    GroupName = s.Group.GroupName,
                    LinkGitHub = s.Group.LinkGitHub,
                    isDeleted = s.Group.isDeleted,
                    isConfirm = s.Group.isConfirm,
                    ResultGrade = s.Group.ResultGrade,
                    ResultScore = s.Group.ResultScore,
                    Enrollments = s.Group.Enrollments.Select(sf => sf.EnrollmentId).ToList(),
                    StudentEmails = s.Group.Enrollments.Select(sf => sf.Student.Email).ToList(),
                    LecturerEmail = s.Group.Lecturer.Email,
                    Comments = s.Group.Board.BoardEnrollments.Select(sf => sf.Comment).ToList(),
                    StudentInformations = s.Group.Enrollments.Select(sf => new StudentInformationResource
                    {
                        Email = sf.Student.Email,
                        Name = sf.Student.Name
                    }).ToList(),
                    UploadFilesInformation = s.Group.UploadedFiles.Select(sf => new UploadFilesInformationResource
                    {
                        UploadedFileId = sf.UploadedFileId,
                        Title = sf.Title,
                        Url = sf.Url
                    }).ToList(),
                    UploadedFiles = s.Group.UploadedFiles.Select(sf => sf.UploadedFileId).ToList(),
                    Tasks = s.Group.Tasks.Select(sf => sf.TaskId).ToList()
                }));

            CreateMap<Recommendation, RecommendationResource>()
                .ForMember(cr => cr.BoardEnrollmentId, opt => opt.MapFrom(c => c.BoardEnrollment.BoardEnrollmentId));

            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            CreateMap<Tag, TagResource>()
                .ForMember(tr => tr.TagProjects, opt => opt.MapFrom(t => t.TagProjects.Select(tf => tf.TagProjectId)));

            CreateMap<TagProject, TagProjectResource>()
                .ForMember(tr => tr.TagId, opt => opt.MapFrom(t => t.Tag.TagId))
                .ForMember(tr => tr.ProjectId, opt => opt.MapFrom(t => t.Project.ProjectId));

            /*///////////////////////// Tasking Feature///////////////////////////////////// */
            CreateMap<Activity, ActivityResource>()
            .ForMember(ar => ar.TaskId, opt => opt.MapFrom(a => a.Task.TaskId));

            CreateMap<Comment, CommentResource>()
            .ForMember(cr => cr.Email, opt => opt.MapFrom(c => c.User.Email))
            .ForMember(cr => cr.TaskId, opt => opt.MapFrom(c => c.Task.TaskId));

            CreateMap<Status, StatusResource>()
             .ForMember(sr => sr.Tasks, opt => opt.MapFrom(s => s.Tasks.Select(sf => sf.TaskId)));

            CreateMap<Models.TaskingModels.Task, TaskResource>()
            //.ForMember(tr => tr.IsLate, opt => opt.MapFrom(t => DateTime.Compare(t.DueDate, DateTime.Now) >= 1 ? false : true))
            .ForMember(tr => tr.IsLate, opt => opt.MapFrom(t => DateTime.Compare(t.DueDate.Date, DateTime.Now.Date)))
            .ForMember(tr => tr.GroupId, opt => opt.MapFrom(t => t.Group.GroupId))
            .ForMember(tr => tr.StatusId, opt => opt.MapFrom(t => t.Status.StatusId))
            .ForMember(tr => tr.Attachments, opt => opt.MapFrom(t => t.Attachments.Select(tf => tf.UploadedFileId)))
            .ForMember(tr => tr.CheckList, opt => opt.MapFrom(t => t.CheckList.Select(tf => tf.TaskItemId)))
            .ForMember(tr => tr.Commnets, opt => opt.MapFrom(t => t.Commnets.Select(tf => tf.CommentId)))
            .ForMember(tr => tr.Activities, opt => opt.MapFrom(t => t.Activities.Select(tf => tf.ActivityId)))
            .ForMember(tr => tr.Members, opt => opt.MapFrom(t => t.Members.Select(tf => tf.Id)));

            CreateMap<TaskItem, TaskItemResource>()
            .ForMember(tr => tr.TaskId, opt => opt.MapFrom(t => t.Task.TaskId));

            //API Resource to domain
            CreateMap<QueryResource, Query>();

            CreateMap<StudentResource, Student>()
            .ForMember(s => s.Id, opt => opt.Ignore())
            .ForMember(s => s.Major, opt => opt.Ignore())
            .ForMember(s => s.Enrollments, opt => opt.Ignore());
            //.AfterMap((sr, s) =>
            //{
            //    //remove unselected enrollments
            //    var removedEnrollments = s.Enrollments.Where(e => !sr.Enrollments.Contains(e.EnrollmentId));
            //    foreach (var e in removedEnrollments)
            //    {
            //        s.Enrollments.Remove(e);
            //    }

            //    //add new enrollments
            //    var addedEnrollments = sr.Enrollments.Where(id => !s.Enrollments.Any(e => e.EnrollmentId == id)).Select(id => new Enrollment { EnrollmentId = id });
            //    foreach (var e in addedEnrollments)
            //    {
            //        s.Enrollments.Add(e);
            //    }
            //});

            CreateMap<AnnouncementResource, Announcement>()
                .ForMember(c => c.AnnouncementId, opt => opt.Ignore());

            CreateMap<AnnouncementUserResource, AnnouncementUser>()
                .ForMember(c => c.AnnouncementUserId, opt => opt.Ignore());

            CreateMap<BoardResource, Board>()
                .ForMember(c => c.BoardId, opt => opt.Ignore())
                .ForMember(c => c.BoardEnrollments, opt => opt.Ignore());

            CreateMap<BoardEnrollmentResource, BoardEnrollment>()
                .ForMember(c => c.Lecturer, opt => opt.Ignore())
                .ForMember(c => c.Recommendations, opt => opt.Ignore())
                .ForMember(c => c.Board, opt => opt.Ignore())
                .ForMember(c => c.BoardEnrollmentId, opt => opt.Ignore());

            CreateMap<GradeResource, Grade>()
                .ForMember(g => g.GradeId, opt => opt.Ignore());

            CreateMap<RoleResource, ApplicationRole>()
                .ForMember(r => r.Id, opt => opt.Ignore());

            CreateMap<EnrollmentResource, Enrollment>()
                .ForMember(e => e.EnrollmentId, opt => opt.Ignore());

            CreateMap<GroupResource, Group>()
                .ForMember(g => g.GroupId, opt => opt.Ignore())
                .ForMember(g => g.Enrollments, opt => opt.Ignore())
                .ForMember(g => g.Project, opt => opt.Ignore())
                .ForMember(g => g.Board, opt => opt.Ignore())
                .ForMember(g => g.Lecturer, opt => opt.Ignore())
                .ForMember(g => g.GroupId, opt => opt.Ignore());

            CreateMap<ProjectResource, Project>()
                 .ForMember(p => p.ProjectId, opt => opt.Ignore())
                 .ForMember(p => p.TagProjects, opt => opt.Ignore())
                 .ForMember(p => p.Major, opt => opt.Ignore());

            CreateMap<LecturerResource, Lecturer>()
                 .ForMember(l => l.LecturerId, opt => opt.Ignore())
                 .ForMember(l => l.Major, opt => opt.Ignore())
                 .ForMember(l => l.BoardEnrollments, opt => opt.Ignore())
                 .ForMember(l => l.Groups, opt => opt.Ignore())
                 .ForMember(l => l.Projects, opt => opt.Ignore());

            CreateMap<MajorResource, Major>()
                 .ForMember(m => m.MajorId, opt => opt.Ignore());

            CreateMap<QuarterResource, Quarter>()
                 .ForMember(m => m.QuarterId, opt => opt.Ignore());
            CreateMap<ExcelResource, Excel>()
                .ForMember(m => m.ExcelId, opt => opt.Ignore());

            // CreateMap<BoardEnrollmentResource, BoardEnrollment>()
            //     .ForMember(m => m.BoardEnrollmentId, opt => opt.Ignore());

            CreateMap<UploadedFileResource, UploadedFile>()
                .ForMember(m => m.UploadedFileId, opt => opt.Ignore());

            CreateMap<TagProjectResource, TagProject>()
                .ForMember(m => m.TagProjectId, opt => opt.Ignore());

            CreateMap<TagResource, Tag>()
                .ForMember(m => m.TagId, opt => opt.Ignore());

            CreateMap<RecommendationResource, Recommendation>()
                .ForMember(m => m.RecommendationId, opt => opt.Ignore());

            CreateMap<CategoryResource, Category>()
                .ForMember(m => m.CategoryId, opt => opt.Ignore());

            /*///////////////////////// Tasking Feature///////////////////////////////////// */
            CreateMap<ActivityResource, Activity>()
                .ForMember(m => m.ActivityId, opt => opt.Ignore());

            CreateMap<CommentResource, Comment>()
                .ForMember(m => m.CommentId, opt => opt.Ignore());

            CreateMap<StatusResource, Status>()
                .ForMember(m => m.StatusId, opt => opt.Ignore());

            CreateMap<TaskResource, Models.TaskingModels.Task>()
                .ForMember(m => m.TaskId, opt => opt.Ignore())
                .ForMember(m => m.CheckList, opt => opt.Ignore())
                .ForMember(m => m.Commnets, opt => opt.Ignore())
                .ForMember(m => m.Status, opt => opt.Ignore())
                .ForMember(m => m.Activities, opt => opt.Ignore())
                .ForMember(m => m.Members, opt => opt.Ignore());

            CreateMap<TaskItemResource, TaskItem>()
                .ForMember(m => m.TaskItemId, opt => opt.Ignore());

        }
    }
}
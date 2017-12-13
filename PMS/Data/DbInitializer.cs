using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Majors.Any())
            {
                return;   // DB has been seeded
            }

            var majors = new Major[]
            {
                new Major{MajorName = "Kỹ Thuật Phần Mềm",MajorCode="CSE1"},
                new Major{MajorName = "Truyền Thông Và Mạng Máy Tính",MajorCode="CSE2"}
            };
            foreach (Major m in majors)
            {
                context.Majors.Add(m);
            }
            context.SaveChanges();

            var quarters = new Quarter[]
            {
                new Quarter{QuarterName="Học Kì 1 - năm 2017-2018",QuarterStart=DateTime.Parse("2017-09-24"),QuarterEnd=DateTime.Parse("2017-12-02")},
                new Quarter{QuarterName="Học Kì 2 - năm 2017-2018",QuarterStart=DateTime.Parse("2017-12-03"),QuarterEnd=DateTime.Parse("2018-02-02")},
                new Quarter{QuarterName="Học Kì 3 - năm 2017-2018",QuarterStart=DateTime.Parse("2018-02-10"),QuarterEnd=DateTime.Parse("2018-05-20")},
                new Quarter{QuarterName="Học Kì Hè - năm 2017-2018",QuarterStart=DateTime.Parse("2018-06-01"),QuarterEnd=DateTime.Parse("2018-08-30")}
            };
            foreach (Quarter q in quarters)
            {
                context.Quarters.Add(q);
            }
            context.SaveChanges();

            var lecturers = new Lecturer[]
            {
                new Lecturer{Name="Lý Mạnh Hùng",DateOfBirth=DateTime.Parse("1980-02-05"),Email="hung.ly@eiu.edu.vn",PhoneNumber="0918843826",Address="Bình DƯơng",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Nguyễn Tuấn Kiệt",DateOfBirth=DateTime.Parse("1985-11-12"),Email="kiet.nguyen@eiu.edu.vn",PhoneNumber="0934843856",Address="Bình Dương",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Trần Nguyện Sơn Quân",DateOfBirth=DateTime.Parse("1980-12-29"),Email="quan.tran@eiu.edu.vn",PhoneNumber="0123843834",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Lê Nhân Văn",DateOfBirth=DateTime.Parse("1986-11-10"),Email="van.le@eiu.edu.vn",PhoneNumber="0969843879",Address="Bình Dương",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Nguyễn Đình Trung",DateOfBirth=DateTime.Parse("1979-02-13"),Email="trung.nguyen@eiu.edu.vn",PhoneNumber="0938843123",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
            };
            foreach (Lecturer s in lecturers)
            {
                context.Lecturers.Add(s);
            }
            context.SaveChanges();

            var projects = new Project[]
            {
                new Project{ProjectCode="1254",Title="Ứng dụng đặt xe trên mobile",Description="Thiết kế ứng dụng trên Android hoặc IOS giúp người dùng có thể xem đặt xe một cách tiện lợi",Type="A",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="1317",Title="WebApplication hỗ trợ đặt món ăn",Description="Thiết kế website giúp tìm và đặt món ăn. Liên kết đến các shipper để giao hành nhanh nhất.",Type="Thực tập tốt nghiệp",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="2001",Title="WebApplication mua bán trực tuyến",Description="Thiết kế website mua bán, tích hợp tài khoản, thanh toán online và quản lý kho hàng cho admin.",Type="B",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="2658",Title="Ứng dụng game cờ vua trên Android",Description="Thiết kế game cờ vua sử dụng AI để phân tích các thế cờ, chiến thuật dựa trên từng người chơi.",Type="Tốt nghiệp",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="1875",Title="Ứng dụng chia sẻ video",Description="Hỗ trợ người dùng chia sẽ video trên Facebook, Youtube và nhiều mạng xã hội khác một cách thuận tiện nhất.",Type="A",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
                new Project{ProjectCode="3002",Title="WebApplication giải toán",Description="Thiết kế website hỗ trợ giải toán. Giải quyết các bài toán về đại số, hình học, toán cao ca61o nhanh nhất và có đưa ra giải thích cho người dùng dễ hiểu.",Type="B",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)}
            };
            foreach (Project s in projects)
            {
                context.Projects.Add(s);
            }
            context.SaveChanges();

            var councilRoles = new CouncilRole[]
            {
                new CouncilRole{CouncilRoleName="President"},
                new CouncilRole{CouncilRoleName="Secretary"},
                new CouncilRole{CouncilRoleName="Supervisor"},
                new CouncilRole{CouncilRoleName="Reviewer"}
            };
            foreach (CouncilRole c in councilRoles)
            {
                context.CouncilRoles.Add(c);
            }
            context.SaveChanges();

            // var roles = new ApplicationRole[] {
            //     new ApplicationRole{Name="Admin", Description="Highest authority with few specifically restricted actions."},
            //     new ApplicationRole{Name="Lecturer", Description="Can create Project and upload files."},
            //     new ApplicationRole{Name="Student", Description="Can only create Enrollment."}

            // };

            // foreach (ApplicationRole r in roles)
            // {
            //     context.ApplicationRole.Add(r);
            // }
            // context.SaveChanges();

            // var user = new ApplicationUser[] {
            //     new ApplicationUser{FullName = "Admin", Email="a@a,com"}
            // };

            // foreach (ApplicationRole r in roles)
            // {
            //     context.Roles.Add(r);
            // }
            // context.SaveChanges();

            //var councils = new Council[]
            //{
            //    new Council{ResultGrade="A" ,ResultScore="92"},
            //    new Council{ResultGrade="B" ,ResultScore="80"},
            //    new Council{ResultGrade="C" ,ResultScore="70"},
            //};
            //foreach (Council s in councils)
            //{
            //    context.Councils.Add(s);
            //}
            //context.SaveChanges();

            //var groups = new Group[]
            //{
            //    new Group{GroupName = "Group1",ProjectId=1,Lecturer=context.Lecturers.SingleOrDefault(lecturer=> lecturer.LecturerId==1),Council=context.Councils.SingleOrDefault(council=>council.CouncilId==1)},
            //    new Group{GroupName = "Group1",ProjectId=2,Lecturer=context.Lecturers.SingleOrDefault(lecturer=> lecturer.LecturerId==2),Council=context.Councils.SingleOrDefault(council=>council.CouncilId==2)},
            //    new Group{GroupName = "Group1",ProjectId=3,Lecturer=context.Lecturers.SingleOrDefault(lecturer=> lecturer.LecturerId==3),Council=context.Councils.SingleOrDefault(council=>council.CouncilId==3)},
            //};
            //foreach (Group s in groups)
            //{
            //    context.Groups.Add(s);
            //}
            //context.SaveChanges();

            //var enrollments = new Enrollment[]
            //{
            //    new Enrollment{StartDate=DateTime.Parse("2017-10-02"),EndDate=DateTime.Parse("2017-12-30"),Group=context.Groups.SingleOrDefault(group => group.GroupId==1),Grade=context.Grades.SingleOrDefault(grade=>grade.GradeId==1)},
            //    new Enrollment{StartDate=DateTime.Parse("2017-10-03"),EndDate=DateTime.Parse("2017-12-31"),Group=context.Groups.SingleOrDefault(group => group.GroupId==2),Grade=context.Grades.SingleOrDefault(grade=>grade.GradeId==2)},
            //     new Enrollment{StartDate=DateTime.Parse("2017-10-04"),EndDate=DateTime.Parse("2017-12-31"),Group=context.Groups.SingleOrDefault(group => group.GroupId==3),Grade=context.Grades.SingleOrDefault(grade=>grade.GradeId==3)}
            //};
            //foreach (Enrollment s in enrollments)
            //{
            //    context.Enrollments.Add(s);
            //}
            //context.SaveChanges();
        }
    }
}
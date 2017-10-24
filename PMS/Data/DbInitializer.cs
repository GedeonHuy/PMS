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
                new Major{MajorName = "Kỹ thuật phần mềm",MajorCode="CSE"},
                new Major{MajorName = "Truyền thông & Mạng máy tính",MajorCode="CSE"}
            };
            foreach (Major m in majors)
            {
                context.Majors.Add(m);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{StudentCode="1331209041",Name="Đàm Đức Duy", DateOfBirth=DateTime.Parse("1995-10-18"),Address="C57/18, khu 5, Chánh Nghĩa, Bình Dương",Email="duy.dam.k3set@eiu.edu.vn", PhoneNumber ="09293841597", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1531200001",Name="Nguyễn Thị Huệ", DateOfBirth=DateTime.Parse("1997-05-19"),Address="121, khu 6, Quận 1, Thành phố Hồ Chí Minh",Email="hue.nguyen.k5set@eiu.edu.vn", PhoneNumber ="09093848857", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="3",IsDeleted=false},
                new Student{StudentCode="1331200002",Name="Lê Trần Diệu Anh", DateOfBirth=DateTime.Parse("1995-12-12"),Address="154/78, khu 3, Phú Cường, Bình Dương",Email="dieu.le.k3set@eiu.edu.vn", PhoneNumber ="01233848985", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1531200008",Name="Trần Đỗ Gia Bảo", DateOfBirth=DateTime.Parse("1997-06-08"),Address="45, khu 3, Quận 2, Thành phố Hồ Chí Minh",Email="bao.tran.k5set@eiu.edu.vn", PhoneNumber ="012338483587", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="3",IsDeleted=false},
                new Student{StudentCode="1331200015",Name="Nguyễn Thành Đạt", DateOfBirth=DateTime.Parse("1995-05-25"),Address="69, khu 7, Chánh Nghĩa, Bình Dương",Email="dat.nguyen.k3set@eiu.edu.vn", PhoneNumber ="09023848598", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1331209040",Name="Đỗ Phúc Điền", DateOfBirth=DateTime.Parse("1995-11-11"),Address="134, khu 7, Quận 3, Thành phố Hồ Chí Minh",Email="dien.do.k3set@eiu.edu.vn", PhoneNumber ="09253848599", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1431200026",Name="Trần Nguyễn Đăng Khoa", DateOfBirth=DateTime.Parse("1996-03-02"),Address="25/69, khu 13, Phú Thọ, Bình Dương",Email="khoa.nguyen.k4set@eiu.edu.vn", PhoneNumber ="01693848372", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="4",IsDeleted=false},
                new Student{StudentCode="1331209023",Name="Lê Minh Triết", DateOfBirth=DateTime.Parse("1995-01-01"),Address="78, khu 1, Quận 1, Thành phố Hồ Chí Minh",Email="triet.le.k3set@eiu.edu.vn", PhoneNumber ="01693842598", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1431200069",Name="Trần Uy Vũ", DateOfBirth=DateTime.Parse("1996-10-19"),Address="C20/18, khu 1, Dĩ An, Bình Dương",Email="vu.tran.k4set@eiu.edu.vn", PhoneNumber ="09363848597", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="4",IsDeleted=false},
                new Student{StudentCode="1331200070",Name="Nguyễn Cao Hải Yến", DateOfBirth=DateTime.Parse("1995-11-06"),Address="A12/23, khu 3, Chánh Nghĩa, Thành phố Hồ Chí Minh",Email="yen.nguyen.k3set@eiu.edu.vn", PhoneNumber ="09293848777", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1331200075",Name="Lê Hạnh San", DateOfBirth=DateTime.Parse("1995-12-19"),Address="158, khu 5, Chánh Nghĩa, Bình Dương",Email="san.le.k3set@eiu.edu.vn", PhoneNumber ="09873848575", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1431209085",Name="Lý Thu Nguyệt", DateOfBirth=DateTime.Parse("1996-12-30"),Address="256, khu 5, Quận 1,Thành phố Hồ Chí Minh",Email="nguyet.ly.k4set@eiu.edu.vn", PhoneNumber ="01693353598", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="4",IsDeleted=false},
                new Student{StudentCode="1331209086",Name="Cao Hải Đăng", DateOfBirth=DateTime.Parse("1995-11-03"),Address="128, khu 5, Phú Cường, Bình Dương",Email="dang.cao.k3set@eiu.edu.vn", PhoneNumber ="02393848575", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1431210001",Name="Nguyễn Minh Nhật", DateOfBirth=DateTime.Parse("1996-07-06"),Address="C57/16, khu 5, Chánh Nghĩa, Bình Dương",Email="nhat.nguyen.k4set@eiu.edu.vn", PhoneNumber ="09593958595", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="4",IsDeleted=false},
                new Student{StudentCode="1331210002",Name="Nguyễn Thanh Liêm", DateOfBirth=DateTime.Parse("1995-06-08"),Address="C58/18, khu 7, Quận 2, Thành phố Hồ Chí Minh",Email="liem.nguyen.k4set@eiu.edu.vn", PhoneNumber ="0985709979", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1331210005",Name="Trần Dạ Thi", DateOfBirth=DateTime.Parse("1995-10-01"),Address="B20/18, khu 7, Dầu Tiếng, Bình Dương",Email="thi.tran.k3set@eiu.edu.vn", PhoneNumber ="09293848709", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="5",IsDeleted=false},
                new Student{StudentCode="1531210007",Name="Nguyễn Ngọc Cát Tường", DateOfBirth=DateTime.Parse("1997-09-12"),Address="53, khu 9, Quận Tân Bình, Thành phố Hồ Chí Minh",Email="truong.gnuyen.k5set@eiu.edu.vn", PhoneNumber ="09693845236", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="3",IsDeleted=false},
                new Student{StudentCode="1331210069",Name="Nguyễn Ngọc Tường Vy", DateOfBirth=DateTime.Parse("1995-01-18"),Address="111, khu 6, Phú Cường, Bình Dương",Email="vy.nguyen.k3set@eiu.edu.vn", PhoneNumber ="01693848597", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="5",IsDeleted=false},
                new Student{StudentCode="1531210070",Name="Đỗ Thị Mỹ Duyên", DateOfBirth=DateTime.Parse("1997-02-18"),Address="125, khu 9, Quận 1,Thành phố Hồ Chí Minh",Email="duyen.do.k5set@eiu.edu.vn", PhoneNumber ="0123321123", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="3",IsDeleted=false},
                new Student{StudentCode="1331210072",Name="Lê Xuân Tùng", DateOfBirth=DateTime.Parse("1995-12-25"),Address="C58/26, khu 6, Phú Thọ, Bình Dương",Email="tung.le.k3set@eiu.edu.vn", PhoneNumber ="0969696969", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="5",IsDeleted=false},
                new Student{StudentCode="1331219045",Name="Nguyễn Thị Tú Âm", DateOfBirth=DateTime.Parse("1995-01-06"),Address="B123, khu 9, Quận 2, Thành phố Hồ Chí Minh",Email="tu.nguyen.k3set@eiu.edu.vn", PhoneNumber ="09218840696", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="5",IsDeleted=false},
                new Student{StudentCode="1431219046",Name="Trần Đỗ Hiền Minh", DateOfBirth=DateTime.Parse("1996-07-20"),Address="C45/20, khu 3, Quận 2, Thành phố Hồ Chí Minh",Email="minh.tran.k4set@eiu.edu.vn", PhoneNumber ="09293848585", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="4",IsDeleted=false},
                new Student{StudentCode="1531219047",Name="Nguyễn Trung Nghĩa", DateOfBirth=DateTime.Parse("1997-08-30"),Address="C78/20, khu 2, Chánh Nghĩa, Bình Dương",Email="nghia.nguyen.k5set@eiu.edu.vn", PhoneNumber ="09793848533", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="3",IsDeleted=false},
                new Student{StudentCode="15312100033",Name="Trần Thiện Tâm", DateOfBirth=DateTime.Parse("1997-02-20"),Address="362, khu 1, Quận Tân Bình, Thành phố Hồ Chí Minh",Email="tam.tran.k5set@eiu.edu.vn", PhoneNumber ="012338596", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="3",IsDeleted=false},
                new Student{StudentCode="1531210099",Name="Nguyễn Hà My", DateOfBirth=DateTime.Parse("1997-10-16"),Address="106, khu 3, Dĩa An, Bình Dương",Email="my.nguyen.k5set@eiu.edu.vn", PhoneNumber ="01693858575", Major=context.Majors.FirstOrDefault(m=>m.MajorId==2), Year="3",IsDeleted=false}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var quarters = new Quarter[]
            {
                new Quarter{QuarterName="HK1",QuarterStart=DateTime.Parse("2017-09-24"),QuarterEnd=DateTime.Parse("2017-12-02")},
                new Quarter{QuarterName="HK2",QuarterStart=DateTime.Parse("2017-12-03"),QuarterEnd=DateTime.Parse("2018-02-02")}
            };
            foreach (Quarter q in quarters)
            {
                context.Quarters.Add(q);
            }
            context.SaveChanges();

            var lecturers = new Lecturer[]
            {
                new Lecturer{Name="Lý Mạnh Hùng",DateOfBirth=DateTime.Parse("1980-02-05"),Email="hung.ly@eiu.edu.vn",PhoneNumber="0918843826",Address="Bình DƯơng",Major=context.Majors.FirstOrDefault(m=>m.MajorId==0)},
                new Lecturer{Name="Nguyễn Tuấn Kiệt",DateOfBirth=DateTime.Parse("1985-11-12"),Email="kiet.nguyen@eiu.edu.vn",PhoneNumber="0934843856",Address="Bình Dương",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Trần Nguyện Sơn Quân",DateOfBirth=DateTime.Parse("1980-12-29"),Email="quan.tran@eiu.edu.vn",PhoneNumber="0123843834",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Lê Nhân Văn",DateOfBirth=DateTime.Parse("1986-11-10"),Email="van.le@eiu.edu.vn",PhoneNumber="0969843879",Address="Bình Dương",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Nguyễn Đình Trung",DateOfBirth=DateTime.Parse("1979-02-13"),Email="trung.nguyen@eiu.edu.vn",PhoneNumber="0938843123",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
                new Lecturer{Name="Trần Quang Vinh",DateOfBirth=DateTime.Parse("1982-11-30"),Email="hung.ly@eiu.edu.vn",PhoneNumber="0123843979",Address="Bình DƯơng",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
            };
            foreach (Lecturer s in lecturers)
            {
                context.Lecturers.Add(s);
            }
            context.SaveChanges();

            var projects = new Project[]
            {
                new Project{ProjectCode="1254",Title="Ứng dụng đặt xe trên mobile",Desciption="Thiết kế ứng dụng trên Android hoặc IOS giúp người dùng có thể xem đặt xe một cách tiện lợi",Type="A",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="1317",Title="WebApplication hỗ trợ đặt món ăn",Desciption="Thiết kế website giúp tìm và đặt món ăn. Liên kết đến các shipper để giao hành nhanh nhất.",Type="Thực tập tốt nghiệp",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="2001",Title="WebApplication mua bán trực tuyến",Desciption="Thiết kế website mua bán, tích hợp tài khoản, thanh toán online và quản lý kho hàng cho admin.",Type="B",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="2658",Title="Ứng dụng game cờ vua trên Android",Desciption="Thiết kế game cờ vua sử dụng AI để phân tích các thế cờ, chiến thuật dựa trên từng người chơi.",Type="Tốt nghiệp",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Project{ProjectCode="1875",Title="Ứng dụng chia sẻ video",Desciption="Hỗ trợ người dùng chia sẽ video trên Facebook, Youtube và nhiều mạng xã hội khác một cách thuận tiện nhất.",Type="A",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
                new Project{ProjectCode="3002",Title="WebApplication giải toán",Desciption="Thiết kế website hỗ trợ giải toán. Giải quyết các bài toán về đại số, hình học, toán cao ca61o nhanh nhất và có đưa ra giải thích cho người dùng dễ hiểu.",Type="B",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)}
            };
            foreach (Project s in projects)
            {
                context.Projects.Add(s);
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
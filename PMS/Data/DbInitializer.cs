using PMS.Models;
using PMS.Models.TaskingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PMS.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async System.Threading.Tasks.Task Initialize()
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
                new Major{MajorName = "Truyền Thông Và Mạng Máy Tính",MajorCode="CSE2"},
                new Major{MajorName = "Điều Dưỡng",MajorCode="CSE3"},
                new Major{MajorName = "Quản Trị Kinh Doanh",MajorCode="CSE4"}
            };
            foreach (Major m in majors)
            {
                context.Majors.Add(m);
            }
            context.SaveChanges();


            var students = new Student[]
            {
                new Student{StudentCode="1331209041",Name="Đàm Đức Duy", DateOfBirth=DateTime.Parse("1995-10-18"),Address="C57/18, khu 5, Chánh Nghĩa, Bình Dương",Email="duy.dam.k3set@eiu.edu.vn", PhoneNumber ="09293841597", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1331200025",Name="Huỳnh Minh Quân", DateOfBirth=DateTime.Parse("1995-10-18"),Address="C57/18, khu 5, Chánh Nghĩa, Bình Dương",Email="quan.huynh.k3set@eiu.edu.vn", PhoneNumber ="09293841597", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
                new Student{StudentCode="1331200025",Name="Nguyễn Quang Huy", DateOfBirth=DateTime.Parse("1995-10-18"),Address="C57/18, khu 5, Chánh Nghĩa, Bình Dương",Email="huy.nguyen.k3set@eiu.edu.vn", PhoneNumber ="09293841597", Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Year="5",IsDeleted=false},
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
            var userGuest = new ApplicationUser
            {
                FullName = "Guest",
                Email = "guest@eiu.edu.vn",
                Avatar = "/assets/images/user.png",
                Major = "",
                UserName = "guest@eiu.edu.vn"
            };
            var passwordGuest = "eiu@123";
            await userManager.CreateAsync(userGuest, passwordGuest);
            await userManager.AddToRoleAsync(userGuest, "Guest"); 
            context.SaveChanges();
  
            foreach (Student student in students)
            {
                var user = new ApplicationUser
                {
                    FullName = student.Name,
                    Email = student.Email,
                    Avatar = "/assets/images/user.png",
                    Major = student.Major.MajorName,
                    UserName = student.Email
                };


                var password = student.StudentCode.ToString(); // Password Default
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Student");

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
                //new Lecturer{Name="Lý Mạnh Hùng",DateOfBirth=DateTime.Parse("1980-02-05"),Email="hung.ly@eiu.edu.vn",PhoneNumber="0918843826",Address="Bình DƯơng",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Nguyễn Công Vũ",DateOfBirth=DateTime.Parse("1958-9-5"),Email="vu.nguyen@eiu.edu.vn",PhoneNumber="0934843856",Address="Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Hà Minh Ngọc",DateOfBirth=DateTime.Parse("1981-12-26"),Email="ngoc.ha@eiu.edu.vn",PhoneNumber="0123843834",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Tất Quảng Phát",DateOfBirth=DateTime.Parse("1985-11-19"),Email="phat.tat@eiu.edu.vn",PhoneNumber="0969843879",Address="Bình Dương",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Lý Văn Hưng",DateOfBirth=DateTime.Parse("1984-10-12"),Email="hung.ly@eiu.edu.vn",PhoneNumber="0969843879",Address="Bình Dương",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1)},
                new Lecturer{Name="Huỳnh Tấn Phước",DateOfBirth=DateTime.Parse("1979-09-01"),Email="phuoc.huynh@eiu.edu.vn",PhoneNumber="0938843123",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
                new Lecturer{Name="Nguyễn Hoàng Sỹ",DateOfBirth=DateTime.Parse("1983-05-31"),Email="sy.nguyen@eiu.edu.vn",PhoneNumber="0938843123",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
                new Lecturer{Name="Đỗ Đình Thuấn",DateOfBirth=DateTime.Parse("1980-05-07"),Email="thuan.do@eiu.edu.vn",PhoneNumber="0938848923",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},
                new Lecturer{Name="Đặng Thái Đoàn",DateOfBirth=DateTime.Parse("1988-06-19"),Email="doan.dang@eiu.edu.vn",PhoneNumber="0938848923",Address="Thành Phố Hồ Chí Minh",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2)},

            };
            foreach (Lecturer s in lecturers)
            {
                context.Lecturers.Add(s);
            }
            context.SaveChanges();

            foreach (Lecturer lecturer in lecturers)
            {
                var user = new ApplicationUser
                {
                    FullName = lecturer.Name,
                    Major = lecturer.Major.MajorName,
                    Avatar = "/assets/images/user.png",
                    Email = lecturer.Email,
                    UserName = lecturer.Email
                };

                var password = "eiu@123"; // Password Default
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Lecturer");

            }
            context.SaveChanges();

            var Tags = new Tag[]
            {
                new Tag{TagName="javascript",TagInfo="JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, weakly-typed language used for both client-side and server-side scripting. Use this tag for questions regarding ECMAScript and its various dialects/implementations (excluding ActionScript and Google-Apps-Script)."},
                new Tag{TagName="java",TagInfo="JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, weakly-typed language used for both client-side and server-side scripting. Use this tag for questions regarding ECMAScript and its various dialects/implementations (excluding ActionScript and Google-Apps-Script)."},
                new Tag{TagName="c#",TagInfo="C# (pronounced \"C sharp\") is a high level, object-oriented programming language that is designed for building a variety of applications that run on the .NET Framework (or .NET Core). C# is simple, powerful, type-safe, and object-oriented."},
                new Tag{TagName="android",TagInfo="Android is Google's mobile operating system, used for programming or developing digital devices (Smartphones, Tablets, Automobiles, TVs, Wear, Glass, IoT). For topics related to Android, use Android-specific tags such as android-intent, not intent, android-activity, not activity, android-adapter, not adapter etc. For questions other than development or programming, but related to Android framework, use the link: https://android.stackexchange.com."},
                new Tag{TagName="asp.net",TagInfo="ASP.NET is a Microsoft web application development framework that allows programmers to build dynamic web sites, web applications and web services. It is useful to use this tag in conjunction with the project type tag e.g. [asp.net-mvc], [asp.net-webforms], or [asp.net-web-api]. Do NOT use this tag for questions about ASP.NET Core - use [asp.net-core] instead."},
                new Tag{TagName="node.js",TagInfo="Node.js is an event-based, non-blocking, asynchronous I/O framework that uses Google's V8 JavaScript engine and libuv library. It is used for developing applications that make heavy use of the ability to run JavaScript both on the client, as well as on server side and therefore benefit from the re-usability of code and the lack of context switching."},
                new Tag{TagName="ios",TagInfo="iOS is the mobile operating system running on the Apple iPhone, iPod touch, and iPad. Use this tag [ios] for questions related to programming on the iOS platform. Use the related tags [objective-c] and [swift] for issues specific to those programming languages."},
                new Tag{TagName="html5",TagInfo="HTML5 (Hyper Text Markup Language, version 5) is an umbrella term for recent web technologies. It is also the latest iteration of HTML. It became a W3C Recommendation in October 2014, introducing new elements and APIs."}
            };
            foreach (Tag t in Tags)
            {
                context.Tags.Add(t);
            }
            context.SaveChanges();

            // var projects = new Project[]
            // {
            //     new Project{ProjectCode="1254",Title="Ứng dụng đặt xe trên mobile",Description="Thiết kế ứng dụng trên Android hoặc IOS giúp người dùng có thể xem đặt xe một cách tiện lợi",Type="Basic Project",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Lecturer =context.Lecturers.FirstOrDefault(m=>m.LecturerId==1)},
            //     new Project{ProjectCode="1317",Title="WebApplication hỗ trợ đặt món ăn",Description="Thiết kế website giúp tìm và đặt món ăn. Liên kết đến các shipper để giao hành nhanh nhất.",Type="Internship",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1), Lecturer =context.Lecturers.FirstOrDefault(m=>m.LecturerId==2)},
            //     new Project{ProjectCode="2001",Title="WebApplication mua bán trực tuyến",Description="Thiết kế website mua bán, tích hợp tài khoản, thanh toán online và quản lý kho hàng cho admin.",Type="Academic Project",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1),Lecturer =context.Lecturers.FirstOrDefault(m=>m.LecturerId==1)},
            //     new Project{ProjectCode="2658",Title="Ứng dụng game cờ vua trên Android",Description="Thiết kế game cờ vua sử dụng AI để phân tích các thế cờ, chiến thuật dựa trên từng người chơi.",Type="Final Project",Major=context.Majors.FirstOrDefault(m=>m.MajorId==1),Lecturer =context.Lecturers.FirstOrDefault(m=>m.LecturerId==3)},
            //     new Project{ProjectCode="1875",Title="Ứng dụng chia sẻ video",Description="Hỗ trợ người dùng chia sẽ video trên Facebook, Youtube và nhiều mạng xã hội khác một cách thuận tiện nhất.",Type="Basic Project",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2),Lecturer =context.Lecturers.FirstOrDefault(m=>m.LecturerId==4)},
            //     new Project{ProjectCode="3002",Title="WebApplication giải toán",Description="Thiết kế website hỗ trợ giải toán. Giải quyết các bài toán về đại số, hình học, toán cao cấp nhanh nhất và có đưa ra giải thích cho người dùng dễ hiểu.",Type="Academic Project",Major=context.Majors.FirstOrDefault(m=>m.MajorId==2),Lecturer =context.Lecturers.FirstOrDefault(m=>m.LecturerId==4)}
            // };
            // foreach (Project s in projects)
            // {
            //     context.Projects.Add(s);
            // }
            // context.SaveChanges();

            var tagProjects = new TagProject[]
            {
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==1), Tag =context.Tags.FirstOrDefault(p => p.TagId==2)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==1), Tag =context.Tags.FirstOrDefault(p => p.TagId==4)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==2), Tag =context.Tags.FirstOrDefault(p => p.TagId==1)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==2), Tag =context.Tags.FirstOrDefault(p => p.TagId==6)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==2), Tag =context.Tags.FirstOrDefault(p => p.TagId==8)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==3), Tag =context.Tags.FirstOrDefault(p => p.TagId==3)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==3), Tag =context.Tags.FirstOrDefault(p => p.TagId==6)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==3), Tag =context.Tags.FirstOrDefault(p => p.TagId==8)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==4), Tag =context.Tags.FirstOrDefault(p => p.TagId==2)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==4), Tag =context.Tags.FirstOrDefault(p => p.TagId==4)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==5), Tag =context.Tags.FirstOrDefault(p => p.TagId==6)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==5), Tag =context.Tags.FirstOrDefault(p => p.TagId==7)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==6), Tag =context.Tags.FirstOrDefault(p => p.TagId==2)},
                new TagProject{Project = context.Projects.FirstOrDefault(p => p.ProjectId==6), Tag =context.Tags.FirstOrDefault(p => p.TagId==8)},
            };
            foreach (TagProject t in tagProjects)
            {
                context.TagProjects.Add(t);
            }
            context.SaveChanges();

            var BoardRoles = new BoardRole[]
            {
                new BoardRole{BoardRoleName="Chair"},
                new BoardRole{BoardRoleName="Secretary"},
                new BoardRole{BoardRoleName="Supervisor"},
                new BoardRole{BoardRoleName="Reviewer"}
            };
            foreach (BoardRole c in BoardRoles)
            {
                context.BoardRoles.Add(c);
            }
            context.SaveChanges();

            var Statuses = new Status[]
            {
                new Status{Title="ToDo"},
                new Status{Title="Doing"},
                new Status{Title="Done"}
            };
            foreach (Status s in Statuses)
            {
                context.Statuses.Add(s);
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

            //var Boards = new Board[]
            //{
            //    new Board{ResultGrade="A" ,ResultScore="92"},
            //    new Board{ResultGrade="B" ,ResultScore="80"},
            //    new Board{ResultGrade="C" ,ResultScore="70"},
            //};
            //foreach (Board s in Boards)
            //{
            //    context.Boards.Add(s);
            //}
            //context.SaveChanges();

            //var groups = new Group[]
            //{
            //    new Group{GroupName = "Group1",ProjectId=1,Lecturer=context.Lecturers.SingleOrDefault(lecturer=> lecturer.LecturerId==1),Board=context.Boards.SingleOrDefault(Board=>Board.BoardId==1)},
            //    new Group{GroupName = "Group1",ProjectId=2,Lecturer=context.Lecturers.SingleOrDefault(lecturer=> lecturer.LecturerId==2),Board=context.Boards.SingleOrDefault(Board=>Board.BoardId==2)},
            //    new Group{GroupName = "Group1",ProjectId=3,Lecturer=context.Lecturers.SingleOrDefault(lecturer=> lecturer.LecturerId==3),Board=context.Boards.SingleOrDefault(Board=>Board.BoardId==3)},
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
import { Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { AdminAuthGuard } from './../core/guards/auth-admin.guard';
import { LecturerAuthGuard } from '../core/guards/auth-lecturer.guard';
import { AdminLecturerAuthGuard } from './../core/guards/auth-admin-lecturer.guard';

export const MainRoutes: Routes = [
    {
        //localhost:xxxx/main
        path: '', component: MainComponent, children: [
            //localhost:xxxx/main
            { path: '', redirectTo: 'main/home', pathMatch: 'full' },
             //localhost:xxxx/main/announcement
             { path: 'announcement', loadChildren: './announcement/announcement.module#AnnouncementModule', canActivate: [AdminAuthGuard] },
            //localhost:xxxx/main/home
            { path: 'home', loadChildren: './home/home.module#HomeModule' },
            //localhost:xxxx/main/user
            { path: 'user', loadChildren: './user/user.module#UserModule' },
            //localhost:xxxx/main/user
            { path: 'quarter', loadChildren: './quarter/quarter.module#QuarterModule', canActivate: [AdminAuthGuard]  },            
            //localhost:xxxx/main/role
            { path: 'role', loadChildren: './role/role.module#RoleModule', canActivate: [AdminAuthGuard] },
            //localhost:xxxx/main/role
            { path: 'major', loadChildren: './major/major.module#MajorModule', canActivate: [AdminAuthGuard] },
            //localhost:xxxx/main/student
            { path: 'student', loadChildren: './student/student.module#StudentModule' },
            //localhost:xxxx/main/student
            { path: 'lecturer', loadChildren: './lecturer/lecturer.module#LecturerModule' },
            //localhost:xxxx/main/project
            { path: 'project', loadChildren: './project/project.module#ProjectModule' },
            //localhost:xxxx/main/group
            { path: 'group', loadChildren: './group/group.module#GroupModule', canActivate: [AdminLecturerAuthGuard] },
            //localhost:xxxx/main/enrollment
            { path: 'enrollment', loadChildren: './enrollment/enrollment.module#EnrollmentModule' },
            //localhost:xxxx/main/confirm-group
            { path: 'confirm-group', loadChildren: './confirm-group/confirm-group.module#ConfirmGroupModule', canActivate: [AdminAuthGuard] },
            //localhost:xxxx/main/group-details/id
            { path: 'group-details/:id', loadChildren: './group-details/group-details.module#GroupDetailsModule' },
            //localhost:xxxx/main/group
            { path: 'group', loadChildren: './group/group.module#GroupModule', canActivate:[AdminLecturerAuthGuard]},
            //localhost:xxxx/main/council
            { path: 'council', loadChildren: './council/council.module#CouncilModule', canActivate:[AdminAuthGuard]},
            //localhost:xxxx/main/enrollment
            { path: 'enrollment', loadChildren: './enrollment/enrollment.module#EnrollmentModule' },
            //localhost:xxxx/main/confirm-group
            { path: 'confirm-group', loadChildren: './confirm-group/confirm-group.module#ConfirmGroupModule', canActivate:[AdminAuthGuard]},       
        ]
    }

]
import { Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { AdminAuthGuard } from './../core/guards/auth-admin.guard';

export const MainRoutes: Routes = [
    {
        //localhost:xxxx/main
        path: '', component: MainComponent, children: [
            //localhost:xxxx/main
            { path: '', redirectTo: 'main/home', pathMatch: 'full' },
            //localhost:xxxx/main/home
            { path: 'home', loadChildren: './home/home.module#HomeModule' },
            //localhost:xxxx/main/user
            { path: 'user', loadChildren: './user/user.module#UserModule' },
            //localhost:xxxx/main/role
            { path: 'role', loadChildren: './role/role.module#RoleModule' , canActivate:[AdminAuthGuard]},   
            //localhost:xxxx/main/role
            { path: 'major', loadChildren: './major/major.module#MajorModule' , canActivate:[AdminAuthGuard]},   
            //localhost:xxxx/main/student
            { path: 'student', loadChildren: './student/student.module#StudentModule'},
            //localhost:xxxx/main/student
            { path: 'lecturer', loadChildren: './lecturer/lecturer.module#LecturerModule'},
            //localhost:xxxx/main/project
            { path: 'project', loadChildren: './project/project.module#ProjectModule' },
            //localhost:xxxx/main/group
            { path: 'group', loadChildren: './group/group.module#GroupModule', canActivate:[AdminAuthGuard]},
            //localhost:xxxx/main/group
            { path: 'enrollment', loadChildren: './enrollment/enrollment.module#EnrollmentModule' },
            //localhost:xxxx/main/group
            { path: 'confirm-group', loadChildren: './confirm-group/confirm-group.module#ConfirmGroupModule', canActivate:[AdminAuthGuard]},       
        ]
    }

]
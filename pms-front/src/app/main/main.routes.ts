import { Routes } from '@angular/router';
import { MainComponent } from './main.component';

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
            { path: 'role', loadChildren: './role/role.module#RoleModule' },   
            //localhost:xxxx/main/role
            { path: 'student', loadChildren: './student/student.module#StudentModule' }                     
        ]
    }

]
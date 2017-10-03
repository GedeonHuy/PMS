import { AuthGuard } from './core/guards/auth.guard';
import { Routes } from '@angular/router';

export const AppRoutes: Routes = [
    //localhost:xxxx
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    //localhost:xxxx/login
    { path: 'login', loadChildren: './login/login.module#LoginModule' },
     //localhost:xxxx/main
    { path: 'main', loadChildren: './main/main.module#MainModule', canActivate: [AuthGuard] }
]
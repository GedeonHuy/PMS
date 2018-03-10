import { AuthGuard } from './core/guards/auth.guard';
import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', loadChildren: 'app/main/login/login.module#LoginModule' },
  { path: 'main', loadChildren: './main/main.module#MainModule', canActivate: [AuthGuard] }

];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: true });

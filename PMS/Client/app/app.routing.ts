import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { AuthGuard } from 'app/core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', loadChildren: 'app/main/login/login.module#LoginModule' },
  { path: 'main', loadChildren: './main/main.module#MainModule', canActivate: [AuthGuard] }

];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: true });

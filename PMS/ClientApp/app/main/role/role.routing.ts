import { RoleComponent } from './role.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: RoleComponent
  }
];

export const routing = RouterModule.forChild(routes);

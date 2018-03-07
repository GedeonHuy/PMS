import { ProjectComponent } from './project.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: ProjectComponent
  }
];

export const routing = RouterModule.forChild(routes);

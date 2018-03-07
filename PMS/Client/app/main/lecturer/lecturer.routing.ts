import { LecturerComponent } from './lecturer.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: LecturerComponent
  }
];

export const routing = RouterModule.forChild(routes);

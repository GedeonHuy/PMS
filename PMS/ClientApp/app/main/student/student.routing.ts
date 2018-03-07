import { Routes, RouterModule }  from '@angular/router';
import { StudentComponent } from './student.component';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: StudentComponent
  }
];

export const routing = RouterModule.forChild(routes);

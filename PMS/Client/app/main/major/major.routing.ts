import { MajorComponent } from './major.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: MajorComponent
  }
];

export const routing = RouterModule.forChild(routes);

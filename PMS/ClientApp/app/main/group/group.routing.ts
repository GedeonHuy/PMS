import { GroupComponent } from './group.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: GroupComponent
  }
];

export const routing = RouterModule.forChild(routes);

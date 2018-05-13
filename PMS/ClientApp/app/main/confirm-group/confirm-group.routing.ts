import { GroupConfirmComponent } from './confirm-group.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: GroupConfirmComponent
  }
];

export const routing = RouterModule.forChild(routes);

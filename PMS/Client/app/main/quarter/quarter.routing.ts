import { QuarterComponent } from './quarter.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: '',
    component: QuarterComponent
  }
];

export const routing = RouterModule.forChild(routes);

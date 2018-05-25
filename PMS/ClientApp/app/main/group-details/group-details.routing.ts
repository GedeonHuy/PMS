import { GroupDetailsComponent } from './group-details.component';
import { Routes, RouterModule }  from '@angular/router';

// noinspection TypeScriptValidateTypes
const routes: Routes = [
    //localhost:xxxx/main/user/index
    { path: '', component: GroupDetailsComponent }
  ]
  

export const routing = RouterModule.forChild(routes);

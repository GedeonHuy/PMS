import { Routes, RouterModule } from "@angular/router";
import { TagDetailsComponent } from "./tag-details.component";

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: "",
    component: TagDetailsComponent
  }
];

export const routing = RouterModule.forChild(routes);

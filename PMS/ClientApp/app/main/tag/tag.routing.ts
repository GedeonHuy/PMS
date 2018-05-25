import { Routes, RouterModule } from "@angular/router";
import { TagComponent } from "./tag.component";

// noinspection TypeScriptValidateTypes
const routes: Routes = [
  {
    path: "",
    component: TagComponent
  }
];

export const routing = RouterModule.forChild(routes);

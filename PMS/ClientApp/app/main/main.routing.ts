import { AuthGuard } from "./../core/guards/auth.guard";
import { Routes, RouterModule } from "@angular/router";
import { Main } from "./main.component";
import { ModuleWithProviders } from "@angular/core";

// noinspection TypeScriptValidateTypes

// export function loadChildren(path) { return System.import(path); };

export const routes: Routes = [
  {
    path: "login",
    loadChildren: "app/main/login/login.module#LoginModule",
    pathMatch: "full"
  },
  {
    path: "register",
    loadChildren: "app/main/register/register.module#RegisterModule"
  },
  {
    path: "main",
    component: Main,
    children: [
      {
        path: "dashboard",
        loadChildren: "./dashboard/dashboard.module#DashboardModule"
      },
      {
        path: "editors",
        loadChildren: "./editors/editors.module#EditorsModule"
      },
      {
        path: "components",
        loadChildren: "./components/components.module#ComponentsModule"
      },
      {
        path: "charts",
        loadChildren: "./charts/charts.module#ChartsModule"
      },
      {
        path: "ui",
        loadChildren: "./ui/ui.module#UiModule"
      },
      {
        path: "forms",
        loadChildren: "./forms/forms.module#FormsModule"
      },
      {
        path: "tables",
        loadChildren: "./tables/tables.module#TablesModule"
      },
      {
        path: "maps",
        loadChildren: "./maps/maps.module#MapsModule"
      },
      {
        path: "student",
        loadChildren: "./student/student.module#StudentModule"
      },
      { path: "role", loadChildren: "./role/role.module#RoleModule" },
      {
        path: "lecturer",
        loadChildren: "./lecturer/lecturer.module#LecturerModule"
      },
      {
        path: "quarter",
        loadChildren: "./quarter/quarter.module#QuarterModule"
      },
      {
        path: "project",
        loadChildren: "./project/project.module#ProjectModule"
      },
      {
        path: "major",
        loadChildren: "./major/major.module#MajorModule"
      },
      {
        path: "group",
        loadChildren: "./group/group.module#GroupModule"
      },
      {
        path: "tag",
        loadChildren: "./tag/tag.module#TagModule"
      },
      {
        path: "tag-details/:tag",
        loadChildren: "./tag-details/tag-details.module#TagDetailsModule"
      },
      {
        path: "confirm-group",
        loadChildren: "./confirm-group/confirm-group.module#GroupConfirmModule"
      },
      {
        path: "group-details/:id",
        loadChildren: "./group-details/group-details.module#GroupDetailsModule"
      }
    ],
    canActivate: [AuthGuard]
  }
];

export const routing: ModuleWithProviders = RouterModule.forChild(routes);

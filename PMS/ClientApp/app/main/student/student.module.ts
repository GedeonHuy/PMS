import { PaginationModule } from './../../core/pagination/pagination.module';
import { NgaModule } from "./../../theme/nga.module";
import { DataService } from "./../../core/services/data.service";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ModalModule } from "ngx-bootstrap/modal";
import { StudentComponent } from "./student.component";
import { routing } from "./student.routing";
import { NotificationService } from "../../core/services/notification.service";

@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    PaginationModule,
    FormsModule,
    routing
  ],
  declarations: [StudentComponent],
  providers: [DataService, NotificationService]
})
export class StudentModule {}

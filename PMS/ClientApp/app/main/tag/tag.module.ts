import { NgaModule } from "./../../theme/nga.module";
import { DataService } from "./../../core/services/data.service";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ModalModule } from "ngx-bootstrap/modal";
import { TagComponent } from "./tag.component";
import { routing } from "./tag.routing";
import { NotificationService } from "../../core/services/notification.service";
@NgModule({
  imports: [
    NgaModule,
    ModalModule.forRoot(),
    CommonModule,
    FormsModule,
    routing
  ],
  declarations: [TagComponent],
  providers: [DataService, NotificationService]
})
export class TagModule {}

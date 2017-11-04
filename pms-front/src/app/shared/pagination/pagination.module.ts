import { PaginationComponent } from './pagination.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
@NgModule({
  imports: [
    CommonModule
  ],
  exports: [PaginationComponent],
  declarations: [PaginationComponent]
})
export class PaginationModule { }

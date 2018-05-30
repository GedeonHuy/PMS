import { PaginationComponent } from './pagination.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [PaginationComponent],
  declarations: [PaginationComponent]
})
export class PaginationModule { }
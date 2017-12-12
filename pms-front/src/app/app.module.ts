import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutes } from './app.routes';
import { AuthGuard } from './core/guards/auth.guard';
import { BrowserXhr } from '@angular/http';
import { BrowserXhrWithProgress, ProgressService } from './core/services/progress.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [
    AuthGuard,
    {provide: BrowserXhr, useClass: BrowserXhrWithProgress},
    ProgressService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
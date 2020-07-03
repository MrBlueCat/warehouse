import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ViewItemsModule } from './_components/view-items/view-items.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms'
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HeaderComponent } from './_components/header/header.component';
import { LoginComponent } from './_components/login/login.component';
import { RegisterComponent } from './_components/register/register.component';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptorService } from './_services/jwt-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ViewItemsModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }

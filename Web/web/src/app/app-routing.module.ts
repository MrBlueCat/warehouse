import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewItemsComponent } from './_components/view-items/view-items.component';
import { LoginComponent } from './_components/login/login.component';
import { RegisterComponent } from './_components/register/register.component';


const routes: Routes = [
  { path: '', component: ViewItemsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

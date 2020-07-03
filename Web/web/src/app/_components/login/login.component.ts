import { Component, ViewChild, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { HttpErrorResponse } from '@angular/common/http';
import { WaitAnimComponent } from '../wait-anim/wait-anim.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  error: string;
  @ViewChild(WaitAnimComponent, { static: true }) processAnim: WaitAnimComponent;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthenticationService,
  ) { }

  ngOnInit() {
    this.processAnim.isProcessing = false;
    this.loginForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });

    if (this.authService.isLoggedIn) {
      this.authService.logout();
      this.router.navigate(['']);
    }
  }

  get login() { return this.loginForm.get('login'); }
  get password() { return this.loginForm.get('password'); }

  async logIn() {
    this.processAnim.isProcessing = true;
    await new Promise(resolve => setTimeout(resolve, 500));
    await this.authService.login(this.login.value, this.password.value).catch(
      (error: HttpErrorResponse) => this.error = error.error);

    this.processAnim.isProcessing = false;
    if (this.authService.isLoggedIn)
      this.router.navigate(['']);
  }
}
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { UserService } from 'src/app/_services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { WaitAnimComponent } from '../wait-anim/wait-anim.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  selectedFile: File;
  imgString: string | ArrayBuffer;

  regirterForm: FormGroup;
  error: any;

  @ViewChild(WaitAnimComponent, { static: true }) processAnim: WaitAnimComponent;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
  ) {
    if (this.authenticationService.isLoggedIn)
      this.router.navigate(['/']);
  }

  ngOnInit() {
    this.processAnim.isProcessing = false;
    this.regirterForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }

  get loginInput() { return this.regirterForm.get('login'); }
  get passwordInput() { return this.regirterForm.get('password'); }
  get confirmPasswordInput() { return this.regirterForm.get('confirmPassword'); }

  get user() {
    return {
      email: this.loginInput.value,
      password: this.passwordInput.value,
      confirmPassword: this.confirmPasswordInput.value,
      avatar: this.imgString
    }
  }

  onFileChanged(img) {
    this.selectedFile = img.target.files[0];
    var reader = new FileReader();
    reader.readAsDataURL(this.selectedFile);
    reader.onload = (res) => {
      this.imgString = reader.result
    };
  }

  async register() {
    this.processAnim.isProcessing = true;

    let res = await this.userService.register(this.user).then(res => {
      this.router.navigate(['/login']);
    }).
      catch((error: HttpErrorResponse) => this.error = error.error);

    this.processAnim.isProcessing = false;
  }

}

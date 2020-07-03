import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(private authService: AuthenticationService) { }

  get isLoggedIn() { return this.authService.isLoggedIn; }

  get firstLoginLetter() { return this.authService.currenUser ? this.authService.currenUser.email.charAt(0).toUpperCase() : ''; }
  get userAvatar() { return this.authService.currenUser ? this.authService.currenUser.avatar : null; }

}

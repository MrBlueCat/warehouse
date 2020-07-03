import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';
import { Role } from '../_models/role.enum';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
  }

  async login(username: string, password: string) {
    return await this.http.post<any>(`${environment.warehouseApiRoot}/account/login`, { email: username, password: password }).toPromise().then(
      (user: User) => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }
      });
  }

  get currenUserToken() {
    return this.currentUserSubject.value ? this.currentUserSubject.value.token : null;
  }

  get currenUser() {
    return this.currentUserSubject.value;
  }

  get isAdmin() {
    return this.currentUserSubject.value && this.currentUserSubject.value.role === Role.Admin;
  }

  get isLoggedIn() {
    return this.currentUserSubject.value !== null;
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

}
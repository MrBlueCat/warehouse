import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { RegisteredUser } from '../_models/registered-user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  async register(user:RegisteredUser) { 
    return await this.http.post(`${environment.warehouseApiRoot}/account/register`, user).toPromise();
  }
}

import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { delay, map } from 'rxjs/operators';

import { of, EMPTY } from 'rxjs';
import moment from 'moment';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
  private baseUrl = 'https://localhost:7091/api/Login';

    constructor(private http: HttpClient,
        @Inject('LOCALSTORAGE') private localStorage: Storage) {
    }
    login(email: string, password: string) {
      return this.http.post<any>(`${this.baseUrl}`, {
        "email": email,
        "password": password
      })
          .pipe(
              map((response) => {
                  // set token property
                  // const decodedToken = jwt_decode(response['token']);
                  // store email and jwt token in local storage to keep user logged in between page refreshes
                  this.localStorage.setItem('currentUser', JSON.stringify({
                      token: response.token,
                      isAdmin: response.isAdmin,
                      email: response.email,
                      id: response.empId,
                      alias: response.email.split('@')[0],
                      expiration: moment().add(1, 'days').toDate(),
                      fullName: response.fullName
                  }));

                  return true;
              }));
  }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.localStorage.removeItem('currentUser');
    }

    getCurrentUser(): any {
        return JSON.parse(this.localStorage.getItem('currentUser')||'{}');
    }

    passwordResetRequest(email: string) {
        return of(true).pipe(delay(1000));
    }

    changePassword(email: string, currentPwd: string, newPwd: string) {
        // return of(true).pipe(delay(1000));
        return this.http.post<any>(`${this.baseUrl}/ChangePassword`, {
          "email": email,
          "oldPassword": currentPwd,
          "newPassword":newPwd
        });
    }

    passwordReset(email: string, token: string, password: string, confirmPassword: string): any {
        return of(true).pipe(delay(1000));
    }
}

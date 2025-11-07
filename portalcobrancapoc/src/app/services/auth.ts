import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private authState = new BehaviorSubject<boolean>(false);
  authState$ = this.authState.asObservable();

  private apiUrl = 'http://localhost:5191/api/account/login/google?returnUrl=http://localhost:4200/clientes';

  constructor(private http: HttpClient) {}

  //login(): Observable<any> {
    //return this.http.post(`${this.apiUrl}`, {  }, { withCredentials: true });
    //window.location.href = `${this.apiUrl}`;
  //}

  login() {
    window.location.href = `${this.apiUrl}`;
  }

  logout(): Observable<any> {
    console.log("Logout");
    return this.http.get('http://localhost:5191/api/account/logout', { withCredentials: true });
  }

  getProfile(): Observable<any> {
    return this.http.get('https://localhost:5001/api/user/profile', { withCredentials: true });
  }

  setAuthState(state: boolean) {
    this.authState.next(state);
  }

  isAuthenticated(): boolean {
    return this.authState.value;
  }
}

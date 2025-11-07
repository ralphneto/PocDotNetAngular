import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authState = new BehaviorSubject<boolean>(false);
  authState$ = this.authState.asObservable();

  constructor() { }

  public setToken(token: string): void {
    // Recomenda-se o sessionStorage se o token deve expirar ao fechar a aba
    // ou localStorage para persistência mais longa
    console.log("setToken");
    localStorage.setItem('google_token', token);
  }

  // Obtém o token armazenado
  public getToken(): string | null {
    return localStorage.getItem('google_token');
  }

  // Remove o token no logout
  public logout(): void {
    localStorage.removeItem('google_token');
  }

  setAuthState(state: boolean) {
    this.authState.next(state);
  }

  isAuthenticated(): boolean {
    return this.authState.value;
  }
}

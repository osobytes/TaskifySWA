import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, firstValueFrom } from 'rxjs';
import { LoggedInUser } from '../models/auth.model';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  public getLoggedInUser(): Observable<LoggedInUser> {
    return this.http.get<LoggedInUser>(`/.auth/me`);
  }

  public logOut(): Observable<void> {
    return this.http.get<void>(`/.auth/logout`);
  }

  public async isLoggedIn(): Promise<boolean> {
    const user = await firstValueFrom(this.getLoggedInUser());
    return user.clientPrincipal != null;
  }

  public login(provider: string){
    location.href = `.auth/login/${provider}`;
  }
}

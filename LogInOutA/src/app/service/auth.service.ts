import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http"

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:7282/api/User/"
  si: any;
  constructor(private http : HttpClient) { }

  signUp(userObject: any) {
    return this.http.post<any>(this.baseUrl + 'register', userObject);
  }
  

  logIn(logInObject:any){
    return this.http.post<any>(this.baseUrl + 'Auth', logInObject);
  }

  logOut(userName:any){
    return this.http.post<any>(this.baseUrl + 'Logout', { userName: userName });
  }

  ShowUserName(users:any[]) {
    return this.http.post<any[]>(this.baseUrl + 'ShowUser', {users});
  }
}

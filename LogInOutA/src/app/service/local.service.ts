import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalService {


  constructor() {}

    public saveData(key: string, username: string) {
      localStorage.setItem("key" , username);
    }
    public getData(username: string ) {
      return localStorage.getItem(username)
    }
    public removeData(username: string) {
      localStorage.removeItem(username);
    }
  
    public clearData() {
      localStorage.clear();
    }
  
  } 



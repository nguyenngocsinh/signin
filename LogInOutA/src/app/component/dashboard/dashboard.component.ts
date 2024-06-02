import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthService } from 'src/app/service/auth.service';
import { Route, Router } from '@angular/router';
import { LocalService } from 'src/app/service/local.service';
import { ObjectUnsubscribedError } from 'rxjs';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit  {
  private listusers: any[] = [];
  username: any ;
  constructor (private fb: FormBuilder, private auth: AuthService, private router: Router, private localStorage: LocalService){}
  ngOnInit(){
    

     this.username = this.localStorage.getData("username");
  

  }

  onLogout() {
    const username = this.localStorage.getData('username'); 
    if (username) {
      console.log(username);
      this.auth.logOut(username).subscribe({
        next: () => {
          this.localStorage.clearData;
          this.router.navigate(['login']);
        },
        error: (err) => {
          
          alert(err?.error.message);
          
        }
      });
    } else {
      console.log('No username data found in localStorage')
    }
  }

  users = [
    {
      username: 'a',
      isactive: 1,
    }
  ]
}

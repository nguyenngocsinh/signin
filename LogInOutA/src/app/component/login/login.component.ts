import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import validateForm from 'src/app/helpers/validateForm';
import { AuthService } from 'src/app/service/auth.service';
import { LocalService } from 'src/app/service/local.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  loginForm!: FormGroup;
  constructor (private fb: FormBuilder, private auth: AuthService, private router: Router, private localStorage: LocalService ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({                                      
        username: ['', Validators.required],
        password: ['', Validators.required]
    })
  }

  hideshowPass: boolean = true;
  ShowPass(): void {
    this.hideshowPass = !this.hideshowPass;
  }

  onLogin()
  {
    if(this.loginForm.valid)
    {

      console.log(this.loginForm.value)

      this.auth.logIn(this.loginForm.value)
      .subscribe({
        next:()=>{
          localStorage.setItem('username', this.loginForm.value.username);
          this.localStorage.saveData;
          this.loginForm.reset();
          this.router.navigate(['dashboard']);
        },
        error: (error) => {
          if (error.status === 404) {
              alert("Tên người dùng không tồn tại hoặc mật khẩu không đúng!");
          }}
      })
    }
    else
    {
      validateForm.validationAllFormField(this.loginForm);
      alert("account or password is incorrect!");
    }
  }


}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import validateForm from 'src/app/helpers/validateForm';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './sigup.component.html',
  styleUrls: ['./sigup.component.css']
})
export class SigupComponent implements OnInit {

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  signUpForm! : FormGroup;


  constructor (private fb: FormBuilder, private auth: AuthService, private router: Router){
  }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      username : new FormControl ('', [
        Validators.required,
        Validators.minLength(8)]

      ),

      password : new FormControl ('',[ 
        Validators.required,
        Validators.minLength(8), 
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/)] ),

      confirmpassword : new FormControl ('', Validators.required)
    },
      {
        validators:this.matchpassword()
      }
  )
  }

  get f()
  {
    return this.signUpForm.controls;
  }


  hideshowPass: boolean = true;
  hideshowConfirmPass: boolean = true;

  ShowPass(): void {
    this.hideshowPass = !this.hideshowPass;
  }
  ShowConfirmPass(): void {
    this.hideshowConfirmPass = !this.hideshowConfirmPass;
  }

  onSignup()
  {
    if(this.signUpForm.valid){
;
      this.auth.signUp(this.signUpForm.value)
      .subscribe({
        next:()=>{
          this.signUpForm.reset();
          this.router.navigate(['login']);
        }
        ,error:(err=>{
          alert(err?.error.message);
      })
    })
    }else{

      validateForm.validationAllFormField(this.signUpForm);
      alert("account or password is incorrect!");
      
    }
  }

  matchpassword() {
    return (formGroup: FormGroup) => {
      const passwordControl = formGroup.controls['password'];
      const confirmPasswordControl = formGroup.controls['confirmpassword'];
  
      if (confirmPasswordControl.errors && confirmPasswordControl.errors['matchpassword']) {
        return;
      }
  
      const passwordValue = passwordControl.value;
      const confirmPasswordValue = confirmPasswordControl.value;
  
      if (!confirmPasswordValue) {
        
        confirmPasswordControl.setErrors({ required: true });

      } else if (passwordValue !== confirmPasswordValue) {

        confirmPasswordControl.setErrors({ matchpassword: true });

      } else {

        confirmPasswordControl.setErrors(null);

      }
    };
  }

}



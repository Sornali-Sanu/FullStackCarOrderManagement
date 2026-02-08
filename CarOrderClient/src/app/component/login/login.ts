
import { Component} from '@angular/core';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm!:FormGroup

  constructor(private fb:FormBuilder,
    private authService:Auth
  )
  {
  // Form group definition
   this.loginForm = this.fb.group({
    email: ['',[Validators.required]],
    password:  ['',[Validators.required]]
  });
}

  
  onLogin() {
    // if (this.loginForm.invalid) {console.log('form invalid')
    //   return;}
    // this.authService.login(this.loginForm.value).subscribe(
    //   {
    //     next:res=>console.log('Login Successful',res),
    //     error:err=>console.log('invalid Credentials',err)
    //   }
    // )
     if (this.loginForm.invalid) {
    console.log('Form invalid');
    const controls = this.loginForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        console.log(`${name} is invalid`);
      }
    }
    return;
  }

  console.log('Form valid, calling API...');
  this.authService.login(this.loginForm.value).subscribe({
    next: res => console.log('API response', res),
    error: err => console.error('API error', err)
  });
    
  //   alert('Login button clicked');
  // console.log(this.loginForm.value);
   
  }
}


import { Component} from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm!:FormGroup

  constructor(private fb:FormBuilder,
    private authService:Auth,
    private router:Router
  )
  {
  // Form group definition
   this.loginForm = this.fb.group({
    email: ['',[Validators.required]],
    password:  ['',[Validators.required]]
  });
}

  
  // onLogin() {
   
  //    if (this.loginForm.invalid) {
  //   console.log('Form invalid');
  //   const controls = this.loginForm.controls;
  //   for (const name in controls) {
  //     if (controls[name].invalid) {
  //       console.log(`${name} is invalid`);
  //     }
  //   }
  //   return;
  // }

  // console.log('Form valid, calling API...');
  // this.authService.login(this.loginForm.value).subscribe({
  //   next: res => console.log('API response', res),
  //   error: err => console.error('API error', err)
  // });
  
   
  // }

  onLogin() {

  if (this.loginForm.invalid) return;

  this.authService.login(this.loginForm.value).subscribe({
    next: res => {
      console.log('Login Success', res);
      this.router.navigate(['/']);
    },
    error: err => {
      console.error('Login Failed', err);
      alert("Invalid Email or Password");
    }
  });
}
}

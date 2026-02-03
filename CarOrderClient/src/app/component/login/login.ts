
import { Component} from '@angular/core';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule,HttpClientModule],
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
    email: ['',[Validators.required,Validators.email]],
    password:  ['',[Validators.required]]
  });
}

  
  onLogin() {
    if (this.loginForm.invalid) return;
    this.authService.login(this.loginForm.value).subscribe(
      {
        next:res=>alert('Login Successful'),
        error:ree=>alert('invalid Credentials')
      }
    )
    
  //   alert('Login button clicked');
  // console.log(this.loginForm.value);
   
  }
}

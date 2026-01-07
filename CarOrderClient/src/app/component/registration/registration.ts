import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule,RouterLink,CommonModule],
  templateUrl: './registration.html',
  styleUrl: './registration.css',
})
export class Registration {
 private auth = inject(Auth);
private router = inject(Router);

  // Form group definition
  registrationForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    comfirmPassword:new FormControl('',[Validators.required])
  });

 onRegister() {
  if (this.registrationForm.valid) {

    this.auth.register(this.registrationForm.value).subscribe({
      next: () => {
        alert('Registration successful');
        this.router.navigate(['/login']);
      },
      error: err => {
        console.error(err);
        alert('Registration failed');
      }
    });

  } else {
    alert('Form invalid');
  }
}

}

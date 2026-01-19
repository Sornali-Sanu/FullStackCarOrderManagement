
import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  // Injecting Services
  private auth = inject(Auth);
  private router = inject(Router);

  // Form group definition
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  onLogin() {
    if (this.loginForm.valid) {
      this.auth.login(this.loginForm.value).subscribe({
        next: () => {
          console.log('Login success');
          //this.router.navigate(['/orders']); 
        },
        error: (err) => {
          console.error(err);
          alert('Invalid Login or Server Error');
        }
      });
    } else {
      alert('Please fill the form correctly');
    }
  }
}

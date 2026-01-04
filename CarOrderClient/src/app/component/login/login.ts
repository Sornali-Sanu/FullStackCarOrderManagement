// import { Component } from '@angular/core';
// import { Auth } from '../../services/auth';
// import { Router } from '@angular/router';
// import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
// @Component({
//   selector: 'app-login',
//   imports: [ReactiveFormsModule],
//   templateUrl: './login.html',
//   styleUrl: './login.css',
// })
// export class Login {
 
//   onLogin() {
//   this.auth.login(this.loginForm.value).subscribe({
//     next: () => this.router.navigate(['/orders']),
//     error: () => alert('Invalid Login')
//   });
// }

// }
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { Auth } from '../../services/auth';


@Component({
  selector: 'app-login',
  standalone: true, // আধুনিক এঙ্গুলারে এটা দরকার
  imports: [ReactiveFormsModule], // ফর্ম ব্যবহারের জন্য এটা অবশ্যই লাগবে
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
          this.router.navigate(['/orders']); // লগইন সফল হলে রিডাইরেক্ট হবে
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

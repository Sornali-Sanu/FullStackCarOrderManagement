import { Component, NgModule, signal } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Auth } from './services/auth';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink,CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('CarOrderClient');
  constructor(public auth:Auth,private router:Router)
  {}
  
  logout()
  {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}

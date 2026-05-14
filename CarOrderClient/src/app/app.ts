import { Component, NgModule, OnInit, signal } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';

import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Auth } from './services/auth';
import { Admin } from './services/admin';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink,CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App  implements OnInit{
  protected readonly title = signal('CarOrderClient');

  constructor(public auth:Auth,private router:Router)
  {}
  isAdmin:boolean=false;
  isLoggedIn:boolean=false;
  ngOnInit(): void {
    const token=localStorage.getItem('token');
    const role=localStorage.getItem('role');
    this.isLoggedIn=!!token;
    if(role==='Admin')
    {
      this.isAdmin=true;
    }

    if(this.auth.getAccessToken())
    {this.auth.renewToken().subscribe(
      {
        next:()=>{
          console.log('token renewed')
        },
        error:()=>{
          this.auth.logout();
        }
      }
    )

    }
  }
  
  logout()
  {
    this.auth.logout();
    localStorage.clear();
    this.isAdmin=false;
    this.isLoggedIn=false;
    this.router.navigate(['/getCar'])
   
  }
}

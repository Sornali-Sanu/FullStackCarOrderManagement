import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { User } from '../../../models/user';
import { UserService } from '../../../services/userService';
import { FormBuilder, FormsModule } from '@angular/forms';
@Component({
  selector: 'app-profile',
  imports: [CommonModule,RouterLink,FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit{
  constructor(
    private userService:UserService,
    private router:Router,
  private fb:FormBuilder)
  {

  }

  user:User[]=[];

  ngOnInit(): void {
    this.getProfile();
    
  }
  activeTab:string='profile'
//  passwordForm = this.fb.group({
//     currentPassword: ['', Validators.required],
//     newPassword: ['', Validators.required],
//     confirmPassword: ['', Validators.required]
//   });
  setTab(tab:string)
  {
    this.activeTab=tab
  }
  
  getProfile() {
   this.userService.geProfileView().subscribe((res:User[])=>{
    this.user=res.map(c=>({
      ...c,profileImageUrl:c.profileImageUrl.startsWith('http')?c.profileImageUrl:`${this.userService.baseUrl}/images/${c.profileImageUrl}`
    }))
   })
  }

}

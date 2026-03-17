import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Applicationuser } from '../../../models/Applicationuser';
import { UserService } from '../../../services/userService';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-profile',
  imports: [CommonModule,ReactiveFormsModule],
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

  user!:Applicationuser;
  profileForm!:FormGroup;
  selectedFile!:File;

  ngOnInit(): void {
    this.initForm();
    this.getProfile();
    
  }
  initForm() {
    this.profileForm=this.fb.group(
      {
    fullName:[''], 
    email:[''],
    userName:[''],
    profileImageUrl: [''] ,
    country:[''],
    city:[''],
    streetAddress:[''],  
    phoneNumber:[''],
    drivingLicenseNumber:[''],
    licenseExpiryDate: [''],
    postalCode:[''],
      }
    )
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
  this.userService.geProfileView().subscribe({
    next: (res: Applicationuser) => {

      if (!res) return;


      this.user = res;

      
      if (this.user.profileImageUrl && !this.user.profileImageUrl.startsWith('http')) {
        this.user.profileImageUrl =
          `${this.userService.baseUrl}/${this.user.profileImageUrl}`;
      }

  
      this.profileForm.patchValue(this.user);
    },
    error: (err) => {
      console.error(err);
    }
  });
}
  onFileSelect(
    event:any
  ){
    //files is an array .selectedFile store here
    this.selectedFile=event.taeget.files[0];
  }

  updateProfile() {
    const formData = new FormData();

    Object.keys(this.profileForm.value).forEach(key => {
      formData.append(key, this.profileForm.value[key]);
    });

    if (this.selectedFile) {
      formData.append('profileImage', this.selectedFile);
    }

    this.userService.updateProfile(formData).subscribe(() => {
      alert("Profile Updated Successfully");
      this.getProfile();
    });
  }


}

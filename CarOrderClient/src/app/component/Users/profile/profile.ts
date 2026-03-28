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
  orders:any[]=[];
  ngOnInit(): void {
    this.initForm();
    this.getProfile();
    this.loadOrder();
  }
  loadOrder() {
    this.userService.getMyOrders().subscribe(
      {next:(res)=>{
        this.orders=res
      },
error:(err)=>{
  console.error('error loading Orders',err)
}
    
    }
    
      
    )
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
      console.log(res);
      if(this.user.licenseExpiryDate)
      {
          this.user.licenseExpiryDate =new Date(this.user.licenseExpiryDate).toISOString().split('T')[0];
    
      }
      
      if (this.user.profileImageUrl && !this.user.profileImageUrl.startsWith('http')) {
        this.user.profileImageUrl =
          `${this.userService.baseUrl}/UserImages/${this.user.profileImageUrl}`;
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
    const input= event.target as HTMLInputElement;
    if(input.files && input.files.length>0){
      //files is an array .selectedFile store here
    this.selectedFile=event.target.files[0];
    }
    
  }

  updateProfile() {
    const formData = new FormData();

    Object.keys(this.profileForm.value).forEach(key => {

      const value=this.profileForm.value[key];
      if(value!==null && value!== undefined)
      {
 formData.append(key,value);
      }

     
    });

    if (this.selectedFile) {
      formData.append('profileImage', this.selectedFile);
    }

    this.userService.updateProfile(formData).subscribe(
      {
        next:()=>{
          alert("Profile Update successfully");
          this.getProfile();
        },
        error:(err)=>{console.error(err.error)}

      }
    );
  }


}

import { Component, inject } from '@angular/core';//this class is a Component.
import { Router } from '@angular/router';
import { ReactiveFormsModule,Validators,FormBuilder, FormGroup } from '@angular/forms';//To build form easly import Formbuilder,Validators=>validation,Reactive formModule=>to show angular it is a reactive form
import { Auth } from '../../services/auth';//api call er service
import { CommonModule } from '@angular/common';//ngFor,ngif use korar jonno

//Decorator=>angular k metadata: 
@Component({
  standalone:true,
  selector: 'app-registration',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './registration.html',
  styleUrl: './registration.css',
})
export class Registration {

registrationForm!:FormGroup;
constructor(
  private fb:FormBuilder,
  private authService:Auth
)
{
// Form group definition
//fb.group=>form Structure
this.registrationForm=this.fb.group({
email:['',[Validators.required,Validators.email]],
password:['',[Validators.required]],
confirmPassword:['',Validators.required]
},
{
  validators:this.passwordMatchValidator
}
)
}
 //password compare:
passwordMatchValidator(form:any)
{
const password=form.get('password')?.value;
const confirmPassword=form.get('confirmPassword')?.value;
return password===confirmPassword ? null : {passwordMismatch:true};
}
  
submit()
{
  if(this.registrationForm.invalid)return;
  //backend data send:
  
  this.authService.register(this.registrationForm.value).subscribe({
    next:res=>alert('Registration Successful'),
    error:err=>{alert(err.error)}
  })
}

}
//.subscribe=>observable response handle

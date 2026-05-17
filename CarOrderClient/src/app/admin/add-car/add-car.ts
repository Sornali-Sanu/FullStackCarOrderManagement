import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { CarService } from '../../services/car-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car',
  imports: [ReactiveFormsModule],
  templateUrl: './add-car.html',
  styleUrl: './add-car.css',
})
export class AddCar {

  carForm!:FormGroup;
  selectedFile:any;
  constructor(
    private fb:FormBuilder,
    private service:CarService,
    private router:Router
  )
  {
    this.carForm=this.fb.group(
      {
        name:['',Validators.required],
        brand:['',Validators.required],
description:['',Validators.required] ,
price:['',Validators.required],

 gearbox:['',Validators.required],
 engine:['',Validators.required],
  color:['',Validators.required],
   fuelType:['',Validators.required], 
  bodyType:['',Validators.required],
   condition:['',Validators.required],
   airCon:['',Validators.required],
   driveType:['',Validators.required]
      }
    )
  }

  // image
  onFileSelected(event:any)
  {
    this.selectedFile=event.target.files[0];

  }

  // submit
  onSubmit()
  {
    const formData= new FormData();
    formData.append('name',this.carForm.value.name);
      formData.append('brand',this.carForm.value.brand);
      formData.append('description',this.carForm.value.description);
      formData.append('price',this.carForm.value.price);
      formData.append('gearbox',this.carForm.value.gearbox);
      formData.append('engine',this.carForm.value.engine);
      formData.append('color',this.carForm.value.color);
      formData.append('fuelType',this.carForm.value.fuelType);
      formData.append('bodyType',this.carForm.value.bodyType);
      formData.append('condition',this.carForm.value.condition);
      formData.append('airCon',this.carForm.value.airCon?'true':'false');
      formData.append('driveType',this.carForm.value.driveType);
      formData.append('profileImage',this.selectedFile);
      this.service.addCar(formData).subscribe({
        next:(res:any)=>{
          alert(res.message);
          this.router.navigate(['/getCar'])
        },
        error:(err)=>{console.log(err.error.errors)}
      });
  }
}

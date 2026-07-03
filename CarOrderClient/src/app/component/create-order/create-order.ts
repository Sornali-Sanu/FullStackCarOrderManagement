import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CarService } from '../../services/car-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Car } from '../../models/car';
import { UserService } from '../../services/userService';
import { OrderService } from '../../services/orderService';
import Swal from 'sweetalert2';
import { Users } from '../../admin/users/users';

@Component({
  selector: 'app-create-order',
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './create-order.html',
  styleUrl: './create-order.css',
})
export class CreateOrder implements OnInit {

  car!:Car;
  orderForm!:FormGroup;
  constructor(private route:ActivatedRoute,
    private router:Router,
    private carService:CarService,
    private userService:UserService,
    private fb:FormBuilder,
    private orderService:OrderService
  )
  {

  }

  ngOnInit(): void {
   
   this.orderForm=this.fb.group(
    {
      fullName:['',Validators.required],
      email:['',Validators.required],
      phoneNumber:['',Validators.required],
      shippingAddress:['',Validators.required],
      quantity:[1,Validators.required]
    }
   )
   const id=this.route.snapshot.params['id'];
   this.getCar(id);
   this.getCurrentUser();
  }
  getCurrentUser() {
    this.userService.geProfileView().subscribe({
      next:(user)=>{
        this.orderForm.patchValue(
          {
            fullName:user.fullName,
            email:user.email,
            phoneNumber:user.phoneNumber,
            shippingAddress:user.streetAddress+user.postalCode+user.city
          }
        )
      },
      error:(err)=>{console.log(err)}
    })
  }
  getCar(id: number) {
    this.carService.getCarById(id).subscribe({
      next:res=>{  res.imageUrl = res.imageUrl
      ? `${this.carService.baseUrl}/images/${res.imageUrl}`
      : 'assets/no-image.png';

      this.car=res;},
      error:err=>{console.log(err)}
    })
  }
  
  get totalprice():number{
   const carPrice = this.car?.price ?? 0;
    const quantity=this.orderForm.get('quantity')?.value??1;
    return carPrice*quantity;
  }
  increaseQuantity() {
  const qty = this.orderForm.get('quantity')?.value || 1;
  this.orderForm.patchValue({
    quantity: qty + 1
  });
}

decreaseQuantity() {
  const qty = this.orderForm.get('quantity')?.value || 1;

  if (qty > 1) {
    this.orderForm.patchValue({
      quantity: qty - 1
    });
  }
}

placeOrder() {

  if (!this.car) {

    Swal.fire(
      'Error',
      'Car information not loaded.',
      'error'
    );

    return;

  }

  if (this.orderForm.invalid) {

    Swal.fire(
      'Warning',
      'Please fill all required fields.',
      'warning'
    );

    return;

  }

 const order = {
  orderId: 0,
  carId: this.car.carId,

  carName: this.car.name,
  brand: this.car.brand,
  carImage: this.car.imageUrl,

  orderDate: new Date().toISOString(),
  status: 'Pending',

  totalprice: this.totalprice,
  quantity: this.orderForm.value.quantity,

  shippingAddress: this.orderForm.value.shippingAddress,
  phoneNumber: this.orderForm.value.phoneNumber,

  paymentMethods: '',

  car: this.car,
  user: {} as Users
};

this.orderService.CreateOrder(order).subscribe({
  next: (res) => {
    // Success
  },
  error: (err) => {
    console.log(err);
  }
});
console.log(order);
  this.orderService.CreateOrder(order).subscribe({

    next: (res) => {

      Swal.fire({

        icon: 'success',

        title: 'Order Placed',

        text: 'Your order has been placed successfully.'

      }).then(() => {

        this.router.navigate(['/my-orders']);

      });

    },

    error: (err) => {

      console.log(err);

      Swal.fire({

        icon: 'error',

        title: 'Order Failed',

        text: 'Unable to place your order.'

      });

    }

  });

}

}

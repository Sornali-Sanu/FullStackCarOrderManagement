import { Component, OnInit } from '@angular/core';
import { Order } from '../../models/Order';
import { OrderService } from '../../services/orderService';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-my-orders',
  imports: [CommonModule],
  templateUrl: './my-orders.html',
  styleUrl: './my-orders.css',
})
export class MyOrders implements OnInit {
  orders:Order[]=[];

  constructor(private orderService:OrderService)
  {}
  ngOnInit(): void {
    this.loadOrders();
  }
  loadOrders() {
    this.orderService.getMyOrders().subscribe(res=>{
      this.orders=res;
      console.log('Orders:',res)
    })
  }
  cancelOrder(orderId:number){
  Swal.fire(
    {
      title:'Cancel Order?',
      text:'Do you really want to cancel this order',
      icon:'warning',
      showCancelButton:true,
      confirmButtonText:'yes',
      cancelButtonText:'No'
    }
  ).then(
    (result)=>{
      if(result.isConfirmed)
      {
        this.orderService.cancelOrder(orderId).subscribe(
          {
            next:(res:any)=>{
              Swal.fire('Canceled',res.message,'success');
              this.loadOrders();
            }
          }
        )
      }
    }
  )
  }

}

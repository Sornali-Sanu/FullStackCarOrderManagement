import { Component, OnInit } from '@angular/core';
import { Order } from '../../models/Order';
import { OrderService } from '../../services/orderService';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
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
  cancelOrder(id:number){
    if(confirm('Cancel this order ?'))
    {
this.orderService.deleteOder(id).subscribe(()=>{
  this.loadOrders();
})
    }
  }

}

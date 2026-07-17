import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AdminOrder } from '../../models/AdminOrder';
import { Admin } from '../../services/admin';
import { OrderService } from '../../services/orderService';
import { Order } from '../../models/Order';

@Component({
  selector: 'app-orders',
  imports: [CommonModule,FormsModule],
  templateUrl: './orders.html',
  styleUrl: './orders.css',
})
export class Orders implements OnInit {
  orders:AdminOrder[]=[];
  
  filteredOrders:AdminOrder[]=[];
  search='';
  status='';
  constructor(private service:Admin){}
  ngOnInit(): void {
    this.loadAllOrders();
  }
  loadAllOrders() {
   this.service.getAllOrders().subscribe({
    next:(res)=>{
        console.log(res);
      this.orders=res;
      this.filteredOrders=res;
    }
   })
  }
  FilterOrders(){

    this.filteredOrders=this.orders.filter(x=>{

      const matchesSearch=

      x.customerName.toLowerCase().includes(this.search.toLowerCase())

      ||

      x.carName.toLowerCase().includes(this.search.toLowerCase());

      const matchesStatus=

      this.status=='' || x.status==this.status;

      return matchesSearch && matchesStatus;

    });

  }
  saveStatus(order:AdminOrder)
  {
    this.service.updateStatus(order.orderId,order.status).subscribe(()=>{
     
      alert("Status Updates");
    })
     console.log(this.orders);
  }
  delete(id:number)
  {
    this.service.deleteOrder(id).subscribe(
      ()=>{
        this.loadAllOrders();
      }
    )
  }

}

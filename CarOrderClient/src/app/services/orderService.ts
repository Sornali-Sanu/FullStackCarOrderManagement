import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../models/Order';

@Injectable({
  providedIn: 'root',
})
export class OrderService
{
  apiUrl="https://localhost:7202/api/orders";
  constructor(private http:HttpClient)
  {

  }
  placeOrder(order:any)
  {
    return this.http.post(this.apiUrl,order);
  }
  getMyOrders()
  {
    return this.http.get<Order[]>(`${this.apiUrl}/my-oders`)
  }
  getAllOrder()
  {
    return this.http.get(this.apiUrl);;

  }
  updateStatus(id:number,status:string)
  {return this.http.put(`${this.apiUrl}/status`,{status})

  }
  deleteOder(id:number)
  {
    return this.http.delete(`${this.apiUrl}/${id}`)
  }

  CreateOrder(order:any)
  {
    return this.http.post(this.apiUrl,order);
  }
}

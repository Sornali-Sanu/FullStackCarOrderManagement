import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdminOrder } from '../models/AdminOrder';

@Injectable({
  providedIn: 'root',
})
export class Admin {
  
  private api='https://localhost:7202/api/admin'

  constructor(private http:HttpClient){}
  //DashBoard:
  getDashBoard()
  {
    return this.http.get(`${this.api}/dashboard`);
  }
  //Get all orders:
  getAllOrders():Observable<AdminOrder[]>
  {
    return this.http.get<AdminOrder[]>(this.api);

  }
  //UpdateStatus:
  updateStatus(id:number,status:string){
    return this.http.put(`${this.api}/${id}/status`,{status:status})
  }
  //DeleteOrder:
  deleteOrder(id:number)
  {
    return this.http.delete(`${this.api}/${id}`)
  }
}

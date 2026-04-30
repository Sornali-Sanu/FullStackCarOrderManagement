import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

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
}

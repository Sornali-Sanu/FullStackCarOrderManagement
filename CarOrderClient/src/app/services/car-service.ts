import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root',
})
export class CarService {
  constructor(private http:HttpClient)
  {}
 baseUrl='https://localhost:7202'
  getCarView():Observable<Car[]>{
    return this.http.get<Car[]>(`${this.baseUrl}/api/Cars`)
  }
  deleteCar(id:number)
  {
    return this.http.delete(`${this.baseUrl}/api/Cars/${id}`)
  }
   getCarById(id: number): Observable<Car> {
    return this.http.get<Car>(`${this.baseUrl}/api/Cars/${id}`);
   }
   searchCars(query:string):Observable<Car[]>
   {
    return this.http.get<Car[]>(`${this.baseUrl}/api/Cars/search?query=${query}`)
   }
   addCar(data:any)
   {
    return this.http.post(`${this.baseUrl}/api/Cars`,data)
   }
  updateCar(id:any,data:any)
  {
    return this.http.put(`${this.baseUrl}/api/Cars/${id}`,data)
  }
}

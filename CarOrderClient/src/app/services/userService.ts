import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Applicationuser} from '../models/Applicationuser';

@Injectable({
  providedIn: 'root',
})
export class UserService {
   constructor(private http:HttpClient)
    {}
   baseUrl='https://localhost:7202'

    geProfileView():Observable<Applicationuser>{
      return this.http.get<Applicationuser>(`${this.baseUrl}/api/Users/profile`)
    }

    updateProfile(data:FormData)
    {
      return this.http.put(`${this.baseUrl}/api/Users/UpdateProfile`,data)
    }
  
}

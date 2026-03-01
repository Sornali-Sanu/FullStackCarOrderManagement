import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
   constructor(private http:HttpClient)
    {}
   baseUrl='https://localhost:7202'

    geProfileView():Observable<User[]>{
      return this.http.get<User[]>(`${this.baseUrl}/api/Users`)
    }
  
}

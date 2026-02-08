import { HttpClient } from '@angular/common/http';
import { Injectable} from '@angular/core';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
private baseUrl='https://localhost:7202/api/Auth';
constructor(private http:HttpClient)
{}

  register(data: any) {
  return this.http.post(`${this.baseUrl}/register`, data);
}
  login(model:any)
  {
    return this.http.post(`${this.baseUrl}/login`,model).pipe(
tap(
  (res:any)=>{
localStorage.setItem('accessToken',res.accessToken);
localStorage.setItem('refreshToken',res.refreshToken);
})
)
}
logout()
{
  const refreshToken=localStorage.getItem('refreshToken');
  return this.http.post(`${this.baseUrl}/logout`,{refreshToken});
}
getAccessToken()
{
  return localStorage.getItem('accessToken')
}
renewToken()
  {

    return this.http.post(`${this.baseUrl}/refresh`,{refreshToken:localStorage.getItem('refreshToken')})
  }
}

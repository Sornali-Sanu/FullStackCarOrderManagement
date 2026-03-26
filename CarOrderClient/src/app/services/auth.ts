import { HttpClient } from '@angular/common/http';
import { Injectable} from '@angular/core';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth  {
private baseUrl='https://localhost:7202/api/Auth';
constructor(private http:HttpClient)
{}

  register(data: any) {
  return this.http.post(`${this.baseUrl}/register`, data);
}
  login(model:any)
  {
    return this.http.post<any>(`${this.baseUrl}/login`,model).pipe(
tap(
  (res)=>{
// localStorage.setItem('accessToken',res.accessToken);
// localStorage.setItem('refreshToken',res.refreshToken);
this.setToken(res.accessToken,res.refreshToken)
localStorage.setItem('userName',res.userName);//to set the userName in localstorage
})
)
}

getAccessToken()
{
  return localStorage.getItem('accessToken')
}
//getThr userName
 getUserName()
  {
    return localStorage.getItem('userName');
  }
renewToken()
  {

    return this.http.post<any>(`${this.baseUrl}/refresh`,{refreshToken:localStorage.getItem('refreshToken')}).pipe(
      tap(res=>this.setToken(res.accessToken,res.refreshToken))
    )
  }
  setToken(accessToken: any, refreshToken: any): void {
    localStorage.setItem('accessToken',accessToken);
    localStorage.setItem('refreshToken',refreshToken)
  }
  
 
  isLoggedIn():boolean
  {
    return !! localStorage.getItem('accessToken')
  }

  logout()
{
  const refreshToken = localStorage.getItem('refreshToken');
  
  this.http.post(`${this.baseUrl}/logout`, { refreshToken }).subscribe({
    next: () => {
      localStorage.clear();
      // Optionally redirect to login
      window.location.href = '/login';
    },
    error: (err) => {
      console.error('Logout error', err);
      localStorage.clear();
      window.location.href = '/login';
    }
  });
}
}

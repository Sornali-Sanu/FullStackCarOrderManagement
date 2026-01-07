import { HttpClient } from '@angular/common/http';
import { Injectable ,inject} from '@angular/core';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private apiUrl='http://localhost:5150/api/Auth';
  private http=inject(HttpClient)

  register(data: any) {
  return this.http.post('${this.apiUrl}/regist', data);
}
  login(model:any)
  {
    return this.http.post(`${this.apiUrl}/login`,model).pipe(
tap(
  (res:any)=>{
localStorage.setItem('token',res.accessToken);
localStorage.setItem('refreshToken',res.refreshToken);



  }

)

    )
  }
  
  renewToken()
  {
    const rfToken= localStorage.getItem('refreshToken');
    return this.http.post(`${this.apiUrl}/refresh?refreshToken=${rfToken}`,{})
  }
}

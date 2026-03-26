import { HttpClient, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Auth } from './services/auth';
import { catchError,throwError,switchMap } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

const authService=inject(Auth);
const http=inject(HttpClient);

const accessToken=authService.getAccessToken();

let cloneReq=req;
if(accessToken)
{
  cloneReq=req.clone(
    {
      setHeaders:{
        Authorization:`Bearer ${accessToken}`
      }
    }
  )
}
   return next(cloneReq).pipe(

    catchError(err => {

      if (err.status === 401) {

        
        return authService.renewToken().pipe(

          switchMap((res: any) => {

          
            localStorage.setItem('accessToken', res.accessToken);
            localStorage.setItem('refreshToken', res.refreshToken);

          
            const newReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${res.accessToken}`
              }
            });

            return next(newReq);
          }),

          catchError(error => {
            authService.logout();
            return throwError(() => error);
          })
        );
      }

      return throwError(() => err);
    })
  );

};

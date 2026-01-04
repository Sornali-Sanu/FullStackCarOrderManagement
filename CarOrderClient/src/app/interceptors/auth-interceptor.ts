import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Auth } from '../services/auth';
import { catchError, switchMap, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => 
  {
    const auth=inject(Auth)
    const token=localStorage.getItem('token');

 let clonedReq = req;
  if (token) {
    clonedReq = req.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
  }

  return next(clonedReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
       
        return auth.renewToken().pipe(
          switchMap((res: any) => {
            localStorage.setItem('token', res.accessToken);
            localStorage.setItem('refreshToken', res.refreshToken);
            return next(req.clone({ setHeaders: { Authorization: `Bearer ${res.accessToken}` } }));
          }),
          catchError((refreshErr) => {
            
            localStorage.clear();
            window.location.href = '/login';
            return throwError(() => refreshErr);
          })
        );
      }
      return throwError(() => error);
    })
  );
};

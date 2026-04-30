import { Routes,RouterModule } from '@angular/router';
import { Login } from './component/login/login';
import { GetCar } from './component/Car/get-car/get-car';
import { Registration } from './component/registration/registration';
import { Details } from './component/Car/details/details';
import { Profile } from './component/Users/profile/profile';
import { adminGuard } from './guards/admin-guard';
import { Dashboard } from './admin/dashboard/dashboard';


export const routes: Routes = [
     //Car:
     {path:'',component:GetCar},
     {path:'getCar',component:GetCar},
     {path:'detailsCar/:id',component:Details},


     //Login
     {path:'login',component:Login},
     {path:'registration',component:Registration},
   
     //user Profile;
     {path:'profile',component:Profile},


     //Admin:
     {
          path:'admin',
          canActivate:[adminGuard],
          children:[
               {
                    path:'dashBoard',component:Dashboard
               },
                 { path: 'cars', component: GetCar },
     //  { path: 'orders', component: OrdersComponent },
      { path: 'users', component: Profile}
          ]
     }
];

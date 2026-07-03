import { Routes,RouterModule } from '@angular/router';
import { Login } from './component/login/login';
import { GetCar } from './component/Car/get-car/get-car';
import { Registration } from './component/registration/registration';
import { Details } from './component/Car/details/details';
import { Profile } from './component/Users/profile/profile';
import { adminGuard } from './guards/admin-guard';
import { Dashboard } from './admin/dashboard/dashboard';
import { ManageCars } from './admin/manage-cars/manage-cars';
import { AddCar } from './admin/add-car/add-car';
import { EditCar } from './admin/edit-car/edit-car';
import { MyOrders } from './component/my-orders/my-orders';
import { CreateOrder } from './component/create-order/create-order';


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
     //My Order:
     {path:'my-orders',component:MyOrders},
     // {path:'order/:id',component:orderCom}


      // ORDER:

     { path: 'order/:id', component: CreateOrder},


     //Admin:
     {
          path:'admin',
          canActivate:[adminGuard],
          children:[
               {
                    path:'dashBoard',component:Dashboard
               },
                 { path: 'cars', component: GetCar },



    
      { path: 'users', component: Profile},
      {path:'manage-cars',component:ManageCars},
      {path:'add-car',component:AddCar},
      {path:'edit-car/:id',component:EditCar}
          ]
     }
];

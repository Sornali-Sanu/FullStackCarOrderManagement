import { Routes,RouterModule } from '@angular/router';
import { Login } from './component/login/login';
import { GetCar } from './component/Car/get-car/get-car';
import { Registration } from './component/registration/registration';
import { Details } from './component/Car/details/details';
import { Profile } from './component/Users/profile/profile';

export const routes: Routes = [
     {path:'',component:GetCar},
    {path:'getCar',component:GetCar},
    { path:'login',component:Login},
     {path:'registration',component:Registration},
     {path:'detailsCar/:id',component:Details},
     //user Profile;
     {path:'profile',component:Profile}
];

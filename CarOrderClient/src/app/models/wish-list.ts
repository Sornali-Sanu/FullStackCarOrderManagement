import { Car } from "./car";
import { Applicationuser } from "./Applicationuser";
export interface WishList {
id:Number,
userId:string, 
carId:Number,
applicationUser:Applicationuser,
car: Car, 
}

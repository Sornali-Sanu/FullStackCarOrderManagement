import { Car } from "./car";

export interface Order {
  orderId: number;
  carId: number;
  carName: string;
  brand: string;
  orderDate: string;
  status: string;
  carImage: string;
  totalPrice:number;
  quantity:number;
  shippingAddress:string;
  phoneNumber:string;
  car:Car;

}
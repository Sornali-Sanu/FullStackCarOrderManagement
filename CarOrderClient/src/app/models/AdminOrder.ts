export interface AdminOrder{
    orderId:number;
    customerName:string;
    email:string;
    phoneNumber:string;
    shippingAddress:string;
    carName:string;
    carImage:string;
    quantity:number;
    totalPrice:number;
    orderDate:Date;
    status:string;
}
import { Address } from "./address";
import { IProduct } from "./product";

export interface IOrder{
    id: number,
    orderItems: IOrderItems[],
    shippingAddress: Address,
    orderDate: Date,
    totalPrice: number
}

export interface IOrderItems {
    unitPrice: number,
    quantity: number,
    product: IProduct
}
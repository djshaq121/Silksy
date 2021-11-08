import { IProduct } from "./product";

export interface IShoppingCart {
    cartItems: ICartItem[]
}

export interface ICartItem {
    productId: number,
    product: IProduct,
    quantity: number
}

export class ShoppingCart implements IShoppingCart {
    cartItems: ICartItem[] = [];
}
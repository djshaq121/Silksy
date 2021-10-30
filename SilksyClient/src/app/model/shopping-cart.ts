import { Product } from "./product";

export interface IShoppingCart {
    cartItems: ICartItem[]
}

export interface ICartItem {
    productId: number,
    product: Product,
    quantity: number
}

export class ShoppingCart implements IShoppingCart {
    cartItems: ICartItem[] = [];
}
import { Address } from "./address";

export class OrderCreateParams {
     
    constructor(paymentIntentId: string, address: Address) {
        this.paymentIntentId = paymentIntentId;
        this.shippingAddress = address
    }
    
    paymentIntentId: string;
    shippingAddress: Address;
}
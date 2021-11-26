import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderParams } from '../model/orderParams';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = "https://localhost:5001/api/"; 
  constructor(private http: HttpClient) { }


  createPaymentIntent() {
    return this.http.post(this.baseUrl + 'Payment/PaymentIntent', {});
  }

  createOrder(orderParams: OrderParams) {
    return this.http.post(this.baseUrl + 'Order/CreateOrder', orderParams);
  }

  getStripePublishKey() {
    return this.http.get(this.baseUrl + 'Payment/PublishKey');
  }
}

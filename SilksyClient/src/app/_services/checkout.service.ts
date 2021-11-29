import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IOrder } from '../model/order';
import { OrderCreateParams } from '../model/orderCreateParams';
import { OrderParams } from '../model/orderParams';
import { PaginatedResult } from '../model/pagination';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = "https://localhost:5001/api/"; 
  paginatedResult: PaginatedResult<IOrder[]> = new PaginatedResult<IOrder[]>();
  constructor(private http: HttpClient) { }

  createPaymentIntent() {
    return this.http.post(this.baseUrl + 'Payment/PaymentIntent', {});
  }

  createOrder(orderCreateParams: OrderCreateParams) {
    return this.http.post(this.baseUrl + 'Order/CreateOrder', orderCreateParams);
  }

  getStripePublishKey() {
    return this.http.get(this.baseUrl + 'Payment/PublishKey');
  }

  getOrder(orderId: number) {
    return this.http.get<IOrder>(this.baseUrl + 'Order/' + orderId);
  }

  getOrders(orderParams: OrderParams) {
    let params = new HttpParams();
    params = params.append('pageNumber', orderParams.pageNumber.toString());
    params = params.append('pageSize', orderParams.pageSize.toString());

    return this.http.get<IOrder[]>(this.baseUrl + 'Order/', {observe: 'response', params}).pipe(
      map((response) => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') != null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return this.paginatedResult;
    }) 
    )
  }
}

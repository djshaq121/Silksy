import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { IOrder } from '../model/order';
import { OrderParams } from '../model/orderParams';
import { IPagination } from '../model/pagination';
import { CheckoutService } from '../_services/checkout.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orderParams: OrderParams = new OrderParams();
  pagination: IPagination;
  orders: IOrder[];

  constructor(private checkoutService: CheckoutService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.checkoutService.getOrders(this.orderParams).subscribe((response) => {
      this.orders = response.result;
      this.pagination = response.pagination;
    }, error => {
      this.toastr.error(error);
    })
  }

  pageChanged(event: any) {
    this.orderParams.pageNumber = event.page
    this.getOrders();
  }

}

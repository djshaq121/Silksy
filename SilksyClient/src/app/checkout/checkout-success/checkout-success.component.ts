import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Address } from 'src/app/model/address';
import { IOrder } from 'src/app/model/order';
import { CheckoutService } from 'src/app/_services/checkout.service';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.scss']
})
export class CheckoutSuccessComponent implements OnInit {

  orderId: number;
  order: IOrder = null;
  constructor(private router: Router, private checkoutService: CheckoutService, private toastr: ToastrService) { 
     this.orderId = this.router.getCurrentNavigation().extras?.state?.orderId;

  }

  ngOnInit(): void {
    if (!this.orderId) {
      this.toastr.error("No order Id found");
      return;
    }
   
    this.getOrder();
  }

  getOrder() {
    this.checkoutService.getOrder(this.orderId).subscribe( order => {
      this.order = order;
    }, error => {
      this.toastr.error(error);
    });
  }

}

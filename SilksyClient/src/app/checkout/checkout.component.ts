import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Address } from '../model/address';
import { OrderCreateParams } from '../model/orderCreateParams';
import { IPaymentIntentModel } from '../model/paymentIntent';
import { AccountService } from '../_services/account.service';
import { CheckoutService } from '../_services/checkout.service';
import { ShoppingCartService } from '../_services/shopping-cart.service';

declare const Stripe;

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  // @ViewChild('cardElement', { static: true }) cardElementRef: ElementRef;
  // //@ViewChild('paymentMessage', { static: true }) paymentMessageElementRef: ElementRef;
    checkoutForm: FormGroup;
  // clientSecret: string;
  // stripePaymentMessage: any;
  // stripe: any;
  // card: any;
  // cardHandler = this.onChange.bind(this);
  // isLoadingPaymentSubmission: boolean = false;
  // paymentIntentId: string = "";
  showPaymentComponent: boolean = false;
  isPaymentValid: boolean = false;

  constructor(private formBuilder: FormBuilder, 
    private checkoutService: CheckoutService, public cartService: ShoppingCartService) { }
  
  ngOnInit(): void {
    this.checkoutForm = this.formBuilder.group({
      address: ['', Validators.required],
      address2: [''],
      country: ['', Validators.required],
      city: ['', Validators.required],
      postcode: ['', Validators.required],
    })
  }
  
   openPaymentComponent() {
    this.showPaymentComponent = true;
   }

  //   this.isLoadingPaymentSubmission = true;
  //   //this.paymentMessageElementRef.nativeElement.textContent = "";

  //   let userEmail: string;
  //   this.accountService.currentUser$.pipe(take(1)).subscribe(user => userEmail = user.email);

  //   try {

  //     const shippingAddress = new Address(this.checkoutForm.get("address").value, this.checkoutForm.get("address2").value, this.checkoutForm.get("country").value,
  //     this.checkoutForm.get("city").value, this.checkoutForm.get("postcode").value);

  //     let orderParams = new OrderCreateParams(this.paymentIntentId, shippingAddress);
  //     let order = await this.createOrder(orderParams);

  //     const {paymentIntent, error} = await this.stripe.confirmCardPayment(this.clientSecret, {
  //       payment_method: {
  //         card: this.card
  //       },
  //     },
  //     )
    
  //     //confirmParams: {
  //       //   receipt_email: userEmail ,
  //       // }
  //     if (paymentIntent) {
  //       this.cartService.deleteShoppingCart();
  //       this.isLoadingPaymentSubmission = false;
  //       this.router.navigate(['checkout/success'], {state: order});
  //       return;
  //     }
  
  //     if (error?.type === "card_error" || error?.type === "validation_error") {
  //       this.stripePaymentMessage = error.message;
  //       //this.paymentMessageElementRef.nativeElement.textContent = error.message;
  //     } else {
  //       this.stripePaymentMessage = "Unexpected error with payment";
  //     }

  //   } catch (error) {
  //    // this.paymentMessageElementRef.nativeElement.textContent = "Unexpected error with payment";
  //    this.stripePaymentMessage = "Unexpected error with payment";
  //     this.isLoadingPaymentSubmission = false;
  //   }

  //   this.isLoadingPaymentSubmission = false;
  // }

}

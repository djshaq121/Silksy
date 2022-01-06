import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { Address } from 'src/app/model/address';
import { OrderCreateParams } from 'src/app/model/orderCreateParams';
import { IPaymentIntentModel } from 'src/app/model/paymentIntent';
import { AccountService } from 'src/app/_services/account.service';
import { CheckoutService } from 'src/app/_services/checkout.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';
import { environment } from 'src/environments/environment';

declare const Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() AddressForm: FormGroup;
  @ViewChild('cardElement', { static: true }) cardElementRef: ElementRef;
  clientSecret: string;
  stripePaymentMessage: any;
  stripe: any;
  card: any;
  cardHandler = this.onChange.bind(this);
  isLoadingPaymentSubmission: boolean = false;
  paymentIntentId: string = "";
  
  constructor(private checkoutService: CheckoutService, private router: Router, public cartService: ShoppingCartService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.stripe = Stripe(environment.stripePublishableKey);
    this.createPaymentIntent();
  }

  createPaymentIntent() {
    if(this.paymentIntentId !== "")
      return;

    this.checkoutService.createPaymentIntent().subscribe((result: IPaymentIntentModel) => {
      this.paymentIntentId = result.paymentIntentId;
      this.initStripe(result.clientSecret);
    })
  }

  initStripe(clientSecret: string) {
    this.clientSecret = clientSecret;
    var elements = this.stripe.elements();
    this.card = elements.create('card', {
      hidePostalCode: true,
    });

    this.card.mount(this.cardElementRef.nativeElement);
    this.card.addEventListener('change', this.cardHandler);
  }

  onChange(event) {
    if(event.error) {
      this.stripePaymentMessage = event.error.message;
    } else {
      this.stripePaymentMessage = null;
    }
  }

  async onConfirmPayment() {
    this.isLoadingPaymentSubmission = true;
      //this.paymentMessageElementRef.nativeElement.textContent = "";
  
      let userEmail: string;
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => userEmail = user.email);
  
      try {
  
        const shippingAddress = new Address(this.AddressForm.get("address").value, this.AddressForm.get("address2").value, this.AddressForm.get("country").value,
        this.AddressForm.get("city").value, this.AddressForm.get("postcode").value);
  
        let orderParams = new OrderCreateParams(this.paymentIntentId, shippingAddress);
        let order = await this.createOrder(orderParams);
  
        const {paymentIntent, error} = await this.stripe.confirmCardPayment(this.clientSecret, {
          payment_method: {
            card: this.card
          },
        },
        )
      
        //confirmParams: {
          //   receipt_email: userEmail ,
          // }
        if (paymentIntent) {
          this.cartService.deleteShoppingCart();
          this.isLoadingPaymentSubmission = false;
          this.router.navigate(['checkout/success'], {state: order});
          return;
        }
    
        if (error?.type === "card_error" || error?.type === "validation_error") {
          this.stripePaymentMessage = error.message;
          //this.paymentMessageElementRef.nativeElement.textContent = error.message;
        } else {
          this.stripePaymentMessage = "Unexpected error with payment";
        }
  
      } catch (error) {
       // this.paymentMessageElementRef.nativeElement.textContent = "Unexpected error with payment";
       this.stripePaymentMessage = "Unexpected error with payment";
        this.isLoadingPaymentSubmission = false;
      }
  
      this.isLoadingPaymentSubmission = false;
  }

  private async createOrder(orderParams: OrderCreateParams) {
    return this.checkoutService.createOrder(orderParams).toPromise();
  } 

}

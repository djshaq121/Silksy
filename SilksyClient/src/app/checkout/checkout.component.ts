import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Address } from '../model/address';
import { OrderParams } from '../model/orderParams';
import { IPaymentIntentModel } from '../model/paymentIntent';
import { AccountService } from '../_services/account.service';
import { CheckoutService } from '../_services/checkout.service';

declare const Stripe;

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit, AfterViewInit {

  @ViewChild('cardElement', { static: true }) cardElementRef: ElementRef;
  //@ViewChild('paymentMessage', { static: true }) paymentMessageElementRef: ElementRef;
  checkoutForm: FormGroup;
  clientSecret: string;
  stripePaymentMessage: any;
  stripe: any;
  card: any;
  cardHandler = this.onChange.bind(this);
  isLoadingPaymentSubmission: boolean = false;
  paymentIntentId: string = "";

  constructor(private formBuilder: FormBuilder, private accountService: AccountService, private checkoutService: CheckoutService) { }
  
  ngOnInit(): void {
    this.checkoutForm = this.formBuilder.group({
      address: ['', Validators.required],
      address2: [''],
      country: ['', Validators.required],
      city: ['', Validators.required],
      postcode: ['', Validators.required],
    })
  }
  
  ngAfterViewInit(): void {
    this.stripe = Stripe(environment.stripePublishableKey);
    this.checkout();
  }
  checkout() {
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

      const shippingAddress = new Address(this.checkoutForm.get("address").value, this.checkoutForm.get("address2").value, this.checkoutForm.get("country").value,
      this.checkoutForm.get("city").value, this.checkoutForm.get("postcode").value);

      let orderParams = new OrderParams(this.paymentIntentId, shippingAddress);
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
      if (paymentIntent?.status === 'succeeded') {
        // TODO - Redirect user to success page
        this.isLoadingPaymentSubmission = false;
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

  private async createOrder(orderParams: OrderParams) {
    return this.checkoutService.createOrder(orderParams).toPromise();
  } 

}

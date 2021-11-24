import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

declare const Stripe;

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {


  stripe = Stripe(environment.stripePublicKey);
  elements;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.checkout();
  }

  checkout() {
    this.http.post('https://localhost:5001/api/' + 'payment/', {}).subscribe((result: any) => {
      this.redirectToStripe(result.checkoutSessionUrl);
    })
  }

  redirectToStripe(checkoutSessionUrl: string) {
    window.location.href = checkoutSessionUrl;

    // const appearance = {
    //   theme: 'stripe',
    // };
    // this.elements = this.stripe.elements({appearance, clientSecret})

    // const paymentElement = this.elements.create("payment");
    // paymentElement.mount("#payment-element");
  }

  async onSubmit(event) {
   
    // let ele = this.elements;
    // const {error} = await this.stripe.confirmPayment({
    //   this.elements,
    //   confirmParams: {
    //     return_url: 'https://localhost:4200/complete',
    //   },
    // });

    // if(error) {
    //   const messageContainer = document.querySelector('#error-message');
    //   messageContainer.textContent = error.message;
    // }
    // console.log("submit")
  }
}

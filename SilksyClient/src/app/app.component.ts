import { Component, OnInit } from '@angular/core';
import { IShoppingCart } from './model/shopping-cart';
import { User } from './model/user';
import { AccountService } from './_services/account.service';
import { ProductService } from './_services/product.service';
import { ShoppingCartService } from './_services/shopping-cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  title = 'SilksyClient';

  constructor(private accountService: AccountService, private cartService: ShoppingCartService) {}

  ngOnInit(): void {
    // Any time the main compoonent is reloaded we check to see if the user is log in
    this.checkAndSetCurrentUser();
    this.checkAndSetShoppingCart();
  }

  checkAndSetCurrentUser() {
    let user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  checkAndSetShoppingCart() {
    // let user: User = JSON.parse(localStorage.getItem('user'));
    // let cart: IShoppingCart = JSON.parse(localStorage.getItem('cart'));
    // if (user) {
    //   this.cartService.getShoppingCart().subscribe();
    //   return;
    // }

      
    // if(cart) {

    // }
  }

}

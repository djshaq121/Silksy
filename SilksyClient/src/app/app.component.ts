import { Component, OnInit } from '@angular/core';
import { IShoppingCart } from './model/shopping-cart';
import { User } from './model/user';
import { AccountService } from './_services/account.service';
import { ProductService } from './_services/product.service';
import { ShoppingCartService } from './_services/shopping-cart.service';
//https://coolors.co/000000-14213d-fca311-e5e5e5-ffffff - color palette
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  title = 'Silksy';

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
    let cart: IShoppingCart = JSON.parse(localStorage.getItem('cart'));
    if (cart) {
      this.cartService.setCurrentCart(cart);
      return;
    }

    let user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.cartService.getShoppingCartFromServer().subscribe();
    }
  }

}

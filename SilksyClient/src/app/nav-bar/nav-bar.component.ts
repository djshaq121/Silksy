import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IShoppingCart } from '../model/shopping-cart';
import { AccountService } from '../_services/account.service';
import { ShoppingCartService } from '../_services/shopping-cart.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  shoppingCart: IShoppingCart;
  shoppingCartTotalLength: number;
  cartSubscription: Subscription;

  constructor(public accountService: AccountService, public ShoppingCartService: ShoppingCartService) { 
  }

  ngOnInit(): void {
    this.cartSubscription = this.ShoppingCartService.currentCart$.subscribe(x => {
      this.shoppingCart = x
      this.shoppingCartTotalLength = this.ShoppingCartService.getCartLength(x)});

  }

  ngOnDestroy(): void {
    this.cartSubscription.unsubscribe();
  }

  logout() {
    this.accountService.logout();
  }

}

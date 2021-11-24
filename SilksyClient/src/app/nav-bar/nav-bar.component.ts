import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  cartSubscription: Subscription;

  constructor(public accountService: AccountService, public shoppingCartService: ShoppingCartService, private router: Router ) { 
  }

  ngOnInit(): void {
    this.cartSubscription = this.shoppingCartService.currentCart$.subscribe(x => {
      this.shoppingCart = x
      });

  }

  ngOnDestroy(): void {
    this.cartSubscription.unsubscribe();
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/products'); // TODO - Change it to home page
  }

}

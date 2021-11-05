import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IShoppingCart } from '../model/shopping-cart';
import { ShoppingCartService } from '../_services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent implements OnInit {

  private sub: Subscription;
  
  constructor(public cartService: ShoppingCartService) { }

  ngOnInit(): void {
    // Maybe call get shopping cart to get the latest cart
  }

}

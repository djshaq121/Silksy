import { Component, OnInit } from '@angular/core';
import { IShoppingCart } from '../model/shopping-cart';
import { ShoppingCartService } from '../_services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent implements OnInit {

  cart: IShoppingCart;
  
  constructor(private cartService: ShoppingCartService) { }

  ngOnInit(): void {
    //Check if you're logged in
  }

}

import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/model/product';
import { ICartItem } from 'src/app/model/shopping-cart';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';
import { ShoppingCartComponent } from '../shopping-cart.component';

@Component({
  selector: 'app-basket-item',
  templateUrl: './basket-item.component.html',
  styleUrls: ['./basket-item.component.scss']
})
export class BasketItemComponent implements OnInit {

  quantityOptions: number[] = [1,2,3,4,5,6,7,8,9,10]
  selectedQuantity: string; // Info - The select changes the type to string so must change it to and from int

  @Input() basketItem: ICartItem;
  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.selectedQuantity = this.basketItem.quantity.toString(); 
  }

  removeItemFromBasket() {
    this.shoppingCartService.removeCartItem(this.basketItem);
  }

  onQuantityChange() {
    this.shoppingCartService.updateCartItemQuantity(this.basketItem, parseInt(this.selectedQuantity));
  }
}

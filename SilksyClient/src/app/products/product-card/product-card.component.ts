import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/model/product';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() product: IProduct;
  constructor(private cartService: ShoppingCartService) { }

  ngOnInit(): void {
  }

  addProductToCart() {
    this.cartService.addProductToCart(this.product).subscribe(res => {
      console.log(res);
    }, err => {
      console.log(err);
    });
  }

}

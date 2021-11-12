import { Component, OnInit, Input } from '@angular/core';
import { timer } from 'rxjs';
import { IProduct } from 'src/app/model/product';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {

  @Input() product: IProduct;
  quantityOptions: number[] = [1,2,3,4,5,6,7,8,9,10];
  selectedQuantity: string = "1";

  itemAdded: boolean = false;
  addCartLoading: boolean = false;

  constructor(private cartService: ShoppingCartService) { }

  ngOnInit(): void {
  }

  addProductToCart() {
    let quantity = parseInt(this.selectedQuantity);
    this.addCartLoading = true;
    this.cartService.addProductToCart(this.product, quantity).subscribe(() => {
      this.addCartLoading = false;
      this.itemAddedSuccessfully();
    });
  }

  itemAddedSuccessfully() {
    this.itemAdded = true
    timer(2000).subscribe(() => {
      this.itemAdded = false;
    })
  }

}

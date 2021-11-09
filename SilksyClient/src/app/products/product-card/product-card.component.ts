import { Component, Input, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { IProduct } from 'src/app/model/product';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';
import { ProductDetailComponent } from '../product-detail/product-detail.component';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() product: IProduct;

  constructor(private cartService: ShoppingCartService, private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  addProductToCart() {
    this.cartService.addProductToCart(this.product).subscribe(res => {
      console.log(res);
    }, err => {
      console.log(err);
    });
  }

  openDetailsPage() {
    this.modalService.show(ProductDetailComponent, { initialState: { product: this.product}})
  }

}

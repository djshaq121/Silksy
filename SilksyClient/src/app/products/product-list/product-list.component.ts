import { Component, OnInit } from '@angular/core';
import { IPagination } from 'src/app/model/pagination';
import { IProduct } from 'src/app/model/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: IProduct[] = [];
  pagination: IPagination;
  pageNumber = 1;
  pageSize = 5;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts(this.pageNumber, this.pageSize).subscribe((response)  => {
        this.products = response.result;
        this.pagination = response.pagination;
      });
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.getProducts();
  }

}

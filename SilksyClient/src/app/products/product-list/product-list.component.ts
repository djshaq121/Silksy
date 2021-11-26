import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, forkJoin } from 'rxjs';
import { IBrand } from 'src/app/model/brand';
import { ICategory } from 'src/app/model/category';
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
  //productParams: ProductParams = new ProductParams();
  sortBy: string;
  brands: IBrand[] = [];
  categories: ICategory[] =[];

  private isFilterLoadingSource = new BehaviorSubject<number>(0);
  isFilterLoading$ = this.isFilterLoadingSource.asObservable();

  constructor(public productService: ProductService) { }

  ngOnInit(): void {
    this.getProducts();

    if(this.brands.length <= 0) {
      this.getBrands();
    }

    if(this.categories.length <= 0) {
      this.getCategories();
    }

    this.sortBy = this.productService.productParams.sort;

  }

  getProducts() {
    this.productService.getProducts().subscribe((response)  => {
        this.products = response.result;
        this.pagination = response.pagination;
      });
  }

  getBrands() {
    this.isFilterLoadingSource.next(+1);
    this.productService.getBrands().subscribe(brands => 
        {
          this.brands = brands
          this.isFilterLoadingSource.next(-1);
        }, error => {
          this.isFilterLoadingSource.next(-1);
        }
      );
  }

  getCategories() {
    this.isFilterLoadingSource.next(+1);
    this.productService.getCategories().subscribe(categories => {
      this.categories = categories;
      this.isFilterLoadingSource.next(-1);
    }, error => {
      this.isFilterLoadingSource.next(-1);
    });
  }

  pageChanged(event: any) {
    this.productService.productParams.pageNumber = event.page
   // this.productParams.pageNumber = event.page;
    this.getProducts();
  }

  onBrandSelected(brandId: number) {
    //this.productParams.brandId = brandId;
    //this.productParams.pageNumber = 1;
    if(brandId === this.productService.productParams.brandId) {
      this.productService.productParams.brandId = null;
    }
    else {
      this.productService.productParams.brandId = brandId;
    }

    this.productService.productParams.pageNumber = 1
    this.getProducts();
  }

  onCategorySelected(categoryId: number) {
    // this.productParams.categoriesId = categoryId;
    // this.productParams.pageNumber = 1;

    if(categoryId === this.productService.productParams.categoriesId) {
      this.productService.productParams.categoriesId = null;
    }
    else {
      this.productService.productParams.categoriesId = categoryId;
    }
    
    this.productService.productParams.pageNumber = 1;
    this.getProducts();
  }

  onSortChange(sortBy) {
    // this.productParams.sort = sortBy;
    // this.productParams.pageNumber = 1;

    this.productService.productParams.sort = sortBy;
    this.productService.productParams.pageNumber = 1;
    this.getProducts();
  }

}

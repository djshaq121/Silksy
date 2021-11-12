import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { IBrand } from '../model/brand';
import { ICategory } from '../model/category';
import { PaginatedResult } from '../model/pagination';
import { IProduct } from '../model/product';
import { ProductParams } from '../model/productParams';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = "https://localhost:5001/api/"; 
  paginatedResult: PaginatedResult<IProduct[]> = new PaginatedResult<IProduct[]>();
  public productParams: ProductParams = new ProductParams();

  constructor(private http: HttpClient) {
    this.productParams.sort = "featured"
   }

  resetParams() {
    this.productParams = new ProductParams();
  }
 
  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'Product/Brands');
  }

  getCategories() {
    return this.http.get<ICategory[]>(this.baseUrl + 'Product/Categories');
  }

  getProducts() {
    let params = new HttpParams();
    
    if (this.productParams?.brandId) {
      params = params.append('brandsId', this.productParams.brandId.toString());
    }
    //  else if(this.productParams?.brandId)
    // {
    //   params = params.append('brandId', productParams.brandId.toString());
    // }

    // if(!(productParams?.brandId)) {
    //   this.productParams.brandId = null;
    // }

    if (this.productParams?.categoriesId) {
      params = params.append('caetgoriesId', this.productParams.categoriesId.toString());
    }

    if (this.productParams?.sort) {
      params = params.append('sort', this.productParams.sort);
    }


    params = params.append('pageNumber', this.productParams.pageNumber.toString());
    params = params.append('pageSize', this.productParams.pageSize.toString());
    
    return this.http.get<IProduct[]>(this.baseUrl + 'Product', {observe: 'response', params}).pipe(
      map((response) => {
          this.paginatedResult.result = response.body;
          if(response.headers.get('Pagination') != null) {
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }

          return this.paginatedResult;
      }) 
    )
  }
}

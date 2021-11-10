import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../model/pagination';
import { IProduct } from '../model/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = "https://localhost:5001/api/"; 
  paginatedResult: PaginatedResult<IProduct[]> = new PaginatedResult<IProduct[]>();

  constructor(private http: HttpClient) { }

  getProducts(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if(page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }
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

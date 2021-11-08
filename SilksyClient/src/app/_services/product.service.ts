import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IProduct } from '../model/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = "https://localhost:5001/api/"; 

  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get<IProduct[]>(this.baseUrl + 'Product').pipe(
      map((products: IProduct[]) => {
          return products;
      }) 
    )
  }
}

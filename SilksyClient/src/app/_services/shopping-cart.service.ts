import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject, Subject } from 'rxjs';
import { catchError, map, take } from 'rxjs/operators';
import { Product } from '../model/product';
import { ICartItem, IShoppingCart, ShoppingCart } from '../model/shopping-cart';
import { User } from '../model/user';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private currentCartSourse = new BehaviorSubject<IShoppingCart>(null);
  currentCart$ = this.currentCartSourse.asObservable()
  baseUrl = "https://localhost:5001/api/"; 
  user: User;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  getShoppingCart() {
    if (this.user !== null) {
      return this.getShoppingCartFromServer();

    } else {
      return of(JSON.parse(localStorage.getItem("cart")));
    }
  }

  private getShoppingCartFromServer() {
    return this.http.get<IShoppingCart>(this.baseUrl + 'ShoppingCart').pipe(
      map((cart: IShoppingCart) => {
        this.currentCartSourse.next(cart);
      })
    )
  }

  getCartLength(cart: IShoppingCart): number {
    let length = 0;
    cart?.cartItems?.forEach( item => length += item.quantity );
    return length;
  }

  addProductToCart(product: Product, quantity: number = 1) {
    const cartItem: ICartItem = {productId: product.id, product: product, quantity: quantity }

    let cart: IShoppingCart;
    this.getShoppingCart().subscribe(sc => cart = sc);

    cart.cartItems = this.addOrUpdateProduct(cart.cartItems, cartItem, quantity);

    if(this.user === null) {
      return of(localStorage.setItem("cart", JSON.stringify(cart)));
    }

    return this.http.post(this.baseUrl + 'ShoppingCart/AddProduct', { productId: product.id, quantity: quantity }).pipe(
      map(() =>{
          this.currentCartSourse.next(cart);
      })
    )
  }

  // private getCart() {
  //   let cart: IShoppingCart;
  //   if (this.user !== null) {
  //     cart = this.currentCartSourse.value;

  //   } else {
  //     cart = JSON.parse(localStorage.getItem("cart"));
  //   }

  //   if (cart === null) {
  //     cart = new ShoppingCart();
  //   }
  //   return cart;
  //}

  private addOrUpdateProduct(cartItems: ICartItem[], itemToAdd: ICartItem, quantity: number): ICartItem[] {
    const index = cartItems.findIndex(i => i.productId === itemToAdd.productId);
    if (index === -1) {
      itemToAdd.quantity = quantity;
      cartItems.push(itemToAdd);
    } else {
      cartItems[index].quantity += quantity;
    }
    return cartItems;
  }
}

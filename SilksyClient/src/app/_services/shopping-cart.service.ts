import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject, Subject } from 'rxjs';
import { catchError, flatMap, map, switchMap, take } from 'rxjs/operators';
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
    this.accountService.currentUser$.subscribe(user => this.onUserChanged(user));
  }

  onUserChanged(user: User) {
    this.user = user;
    if (this.user) {
      //Check if we data in 
      let cart: IShoppingCart = JSON.parse(localStorage.getItem('cart'));
      if (cart) {
        this.updateCart(cart).subscribe((response) => {
          localStorage.removeItem('cart');
        }, error => {
          console.log("Failed to update cart")
        })
      }
    } else {
      //If we logged out clear cart
      localStorage.removeItem('cart');
      this.setCurrentCart(null);
    }
  }

  getShoppingCart() {
    if (this.user !== null) {
      return this.getShoppingCartFromServer();

    } else {
      return of(JSON.parse(localStorage.getItem("cart")));
    }
  }

  setCurrentCart(cart: IShoppingCart) {
    this.currentCartSourse.next(cart);
  }

  getShoppingCartFromServer() {
    return this.http.get<IShoppingCart>(this.baseUrl + 'ShoppingCart').pipe(
      map((cart: IShoppingCart) => {
        this.currentCartSourse.next(cart);
        return cart;
      })
    )
  }

  getCartLength(cart: IShoppingCart): number {
    let length = 0;
    cart?.cartItems?.forEach(item => length += item.quantity);
    return length;
  }

  addProductToCart(product: Product, quantity: number = 1) {
    const cartItem: ICartItem = { productId: product.id, product: product, quantity: quantity }

    let cart: IShoppingCart = this.currentCartSourse.value;
    if(cart === null) {
      cart = new ShoppingCart();
    }

   return this.addProduct(cart, cartItem);
    
  }

  private addProduct(cart: IShoppingCart, cartItemToAdd: ICartItem) {
    cart.cartItems = this.addOrUpdateProduct(cart.cartItems, cartItemToAdd);

    if (this.user === null) {
      this.currentCartSourse.next(cart);
      return of(localStorage.setItem("cart", JSON.stringify(cart)));
    }

    return this.updateCart(cart);
    
  }

  private updateCart(cart: IShoppingCart) {
    return this.http.post(this.baseUrl + 'ShoppingCart/UpdateCart', cart).pipe(
      map((response: IShoppingCart) => {
        this.currentCartSourse.next(response);
      })
    )
  }

  private addOrUpdateProduct(cartItems: ICartItem[], itemToAdd: ICartItem): ICartItem[] {
    const index = cartItems.findIndex(i => i.productId === itemToAdd.productId);
    if (index === -1) {
      cartItems.push(itemToAdd);
    } else {

      cartItems[index].quantity += itemToAdd.quantity;
    }
    return cartItems;
  }
}


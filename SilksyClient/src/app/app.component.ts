import { Component, OnInit } from '@angular/core';
import { User } from './model/user';
import { AccountService } from './_services/account.service';
import { ProductService } from './_services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  title = 'SilksyClient';

  constructor(private accountService: AccountService, private p: ProductService) {

  }

  ngOnInit(): void {
    // Any time the main compoonent is reloaded we check to see if the user is log in
    this.checkAndSetCurrentUser();
    this.p.getProducts().subscribe();
  }

  
  checkAndSetCurrentUser() {
    let user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

}

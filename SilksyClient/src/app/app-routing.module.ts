import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { RegisterComponent } from './register/register.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { AlreadySignedInGuard } from './_guards/already-signed-in.guard';

const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AlreadySignedInGuard],
    children: [
      {path: "login", component: LoginComponent},
      {path: "register", component: RegisterComponent},
    ]
  },
  {path: "products", component: ProductListComponent},
  {path: "cart", component: ShoppingCartComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

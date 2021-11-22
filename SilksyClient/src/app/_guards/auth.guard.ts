import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private router: Router) {

  }

  canActivate(next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if(user) {
          return true;
        }

        //this.router.navigateByUrl('/login', queryParams:{'redirectURL':state.url}} )
        console.log(state.url);
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
   
        return false;
      })
    )
  }
  
}

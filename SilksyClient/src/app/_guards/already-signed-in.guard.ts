import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AlreadySignedInGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService) {

  }

  canActivate():  Observable<boolean> {
      return this.accountService.currentUser$.pipe(
        map(user => {
          if(user) {
            this.toastr.error('Already Signed in');
            return false;
          }
  
          return true;
        })
      )
  }
  
}

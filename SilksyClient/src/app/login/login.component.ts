import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { pipe } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
 
  loginForm: FormGroup;
  redirectUrl: string = null;

  constructor(private accountService: AccountService, private formBuilder: FormBuilder, private tostrService: ToastrService, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.redirectUrl = navigation?.extras?.queryParams?.returnUrl;
  }

  ngOnInit(): void {
    this.initForm();
  }

  initForm () {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  login() {
    this.accountService.login(this.loginForm.value).subscribe( () => {
      this.tostrService.success("Signed in successfully");
      this.router.navigateByUrl(this.redirectUrl || '/products');
      
    }, err => {
      this.tostrService.error(err?.error);
    })

    // This is temp - Once the user logs in they should be redirected to the home page be default or 
    // the page they tried to visit dynamically
    this.loginForm.reset();
  }

}

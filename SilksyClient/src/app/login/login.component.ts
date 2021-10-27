import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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

  constructor(private accountService: AccountService, private formBuilder: FormBuilder, private tostrService: ToastrService) { }

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
    this.accountService.login(this.loginForm.value).subscribe( response => {
      this.tostrService.success("Sign in")
    }, err => {
      this.tostrService.error(err?.error);
    })

    // This is temp - Once the user logs in they should be redirected to the home page be default or 
    // the page they tried to visit dynamically
    this.loginForm.reset();
  }

}

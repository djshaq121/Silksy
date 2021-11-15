import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  constructor(private accountService: AccountService, private formBuilder: FormBuilder,
     private tostrService: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    })
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe(
      response => {
        this.tostrService.success("registration complete");
        this.router.navigateByUrl('/products');
      }, 
      error => {
        this.tostrService.success(error?.error);
      }
    )
  }


}

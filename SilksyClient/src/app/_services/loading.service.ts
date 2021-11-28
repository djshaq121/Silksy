import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  request: number = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  loading() {
    this.request++;
    this.spinnerService.show(undefined, {
      type: "line-scale-pulse-out",
      bdColor: 'rgba(192,192,192, 0.5)',
      color: '#333333'
    })
  }

  complete() {
    this.request--;
    if(this.request <= 0) {
      this.request = 0;
      this.spinnerService.hide();
    }
  }
}
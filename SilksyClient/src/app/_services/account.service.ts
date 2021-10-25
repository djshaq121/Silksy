import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
 
  baseUrl = "https://localhost:5001/api/"; 
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model)
  }

  weatherGet() {
    return this.http.get('https://localhost:5001/WeatherForecast').pipe(
      map((resp) => {
          console.log(resp);
    }
    ));
  }
}

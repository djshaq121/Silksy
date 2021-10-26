import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
 
  baseUrl = "https://localhost:5001/api/"; 
  constructor(private http: HttpClient) { }

  login(model: User) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((user: User) => {
        if(user.token) {
          localStorage.setItem('user', JSON.stringify(user));
        }
      })
    )
  }

  register(model: User) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if(user.token) {
          localStorage.setItem('user', JSON.stringify(user))
        }
      })
    )
  }

  weatherGet() {
    return this.http.get('https://localhost:5001/WeatherForecast').pipe(
      map((resp) => {
          console.log(resp);
    }
    ));
  }
}

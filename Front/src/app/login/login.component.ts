import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  invalidLogin!: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  login(form: NgForm){
    const credentials = JSON.stringify(form.value);

    console.log(credentials);

    this.http.post("https://localhost:5001/api/Conta/login", credentials, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      }),
      responseType: 'text'
    })
    .subscribe({
      next: response => {
      const token = (<any>response);
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["/"]);

      let decodedJwt = JSON.parse(window.atob(token.split('.')[1]));
      const decodedRole = decodedJwt["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      const decodedName = decodedJwt["nome"];
      localStorage.setItem("role", decodedRole);
      localStorage.setItem("name", decodedName);
      }, error: e => {
      this.invalidLogin = true;
      console.log(e)
      }
    });
  
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent {
  public role!: any;
  public nome!: any;

  constructor(private router: Router, private jwtHelper: JwtHelperService) { }

  isUserAuthenticated() {
    this.role = localStorage.getItem("role");
    this.nome = localStorage.getItem("name");
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      localStorage.removeItem("jwt");
      localStorage.removeItem("role");
      localStorage.removeItem("name");
      return false;
    }
  }

  logOut() {
    localStorage.removeItem("jwt");
    localStorage.removeItem("role");
    localStorage.removeItem("name");
    this.router.navigate(["home"]);
  }

}

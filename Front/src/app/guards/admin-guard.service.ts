import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AdminGuardService implements CanActivate {

  constructor(private router: Router, private jwtHelper: JwtHelperService) { }

  canActivate() {
    const token = localStorage.getItem("jwt");
    const role = localStorage.getItem("role");

    if(token && !this.jwtHelper.isTokenExpired(token) && role == "Admin") {
      return true;
    }
    this.router.navigate(["login"]);
    return false;
  }

}

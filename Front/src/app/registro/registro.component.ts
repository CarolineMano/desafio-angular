import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorService } from '../error/error.service';
import { ErrorResponse } from '../error/errorResponse';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html'
})
export class RegistroComponent {
  errMsg: any;
  errorString: any;
  existeErro!: boolean;

  constructor(private router: Router, private errorService: ErrorService, private http: HttpClient) { }

  register(form: NgForm) {
    this.existeErro = false;
    const registrationData = JSON.stringify(form.value);

    this.http.post("https://localhost:5001/api/Conta/registrar", registrationData, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      }),
      responseType: 'text'
    })
    .subscribe({
      next: response => {
        const resp = (<any>response);
        this.router.navigate(["login"]);
      }, error: e => {
        this.errMsg = e as ErrorResponse;

        this.errorString = this.errMsg.error;
        console.log(this.errorString)
        this.errorString = this.errorService.stringifyError(this.errorString)
        this.existeErro = true;
      }
    })
  }
}

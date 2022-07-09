import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Endereco } from './endereco';

@Injectable()
export class EnderecoService {

  constructor(private http: HttpClient) { }

  protected EnderecoConsultaCep = "https://api.invertexto.com/v1/cep/";
  protected TokenConsultaCep = "?token=333%7Ctb08rjo5vlly8JLk3qJbMkE1y4PKuoIZ";

  pegarEndereco(cep: string): Observable<any> {
    return this.http.get(this.EnderecoConsultaCep + cep + this.TokenConsultaCep);
  }
}

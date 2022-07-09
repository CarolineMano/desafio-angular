import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto } from './produto';

@Injectable()
export class ProdutoService {

  constructor(private http: HttpClient) {}

  protected UrlService: string = "https://localhost:5001/api/Produto"

  pegarProdutos() : Observable<Produto[]> {
    return this.http.get<Produto[]>(this.UrlService);
  }

  adicionarProduto(produto: any) : Observable<any> {
    return this.http.post(this.UrlService, produto, {
      responseType: 'text'
    })
  }

  excluirProduto(id: any) : Observable<any> {
    return this.http.delete(this.UrlService + "/" + id);
  }

  pegarProduto(id: any) : Observable<any> {
    return this.http.get<Produto>(this.UrlService + "/" + id);
  }

  editarProduto(id: any, produto: any) : Observable<any> {
    return this.http.patch(this.UrlService + "/" + id, produto, {
      responseType: 'text'
    })
  }
}

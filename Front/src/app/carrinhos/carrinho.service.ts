import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemCarrinho } from './itemCarrinho';

@Injectable()
export class CarrinhoService {

  constructor(private http: HttpClient) { }

  protected UrlService: string = "https://localhost:5001/api/MeuCarrinho"

  pegarItensCarrinho() : Observable<ItemCarrinho[]> {
    return this.http.get<ItemCarrinho[]>(this.UrlService + '/Itens');
  }

  pegarValorCarrinho() : Observable<any> {
    return this.http.get<any>(this.UrlService)
  }

  pegarCarrinhosFinalizados() : Observable<any> {
    return this.http.get<any>(this.UrlService + "/Todos")
  }

  adicionarItemCarrinho(id: number,  quantidade: number) : Observable<any>{
    return this.http.post(this.UrlService + "/AdicionarProduto/" + id, {quantidade: quantidade}, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      }),
      responseType: "text"
    })
  }

  excluirItemCarrinho(id: number) : Observable<any>{
    return this.http.delete(this.UrlService + "/ExcluirProduto/" + id, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      }),
      responseType: "text"
    })
  }

  comprarCarrinho(endereco: any) : Observable<any>{
    return this.http.post(this.UrlService + "/comprar", endereco, {
      headers: new HttpHeaders({
        "Content-type": "application/json"
      }),
      responseType: 'text'
    })
  }
}
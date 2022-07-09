import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CarrinhoService } from 'src/app/carrinhos/carrinho.service';
import { Produto } from '../produto';
import { ProdutoService } from '../produto.service';

@Component({
  selector: 'app-lista-produto',
  templateUrl: './lista-produto.component.html'
})
export class ListaProdutoComponent implements OnInit {

  public produtos!: Produto[];
  public role = localStorage.getItem("role");

  constructor(private produtoService: ProdutoService, private carrinhoService: CarrinhoService, private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    this.produtoService.pegarProdutos()
      .subscribe(produtos => {
        this.produtos = produtos;
      })
  }

  adicionarAoCarrinho(id: number,  quantidade: any) {
      this.carrinhoService.adicionarItemCarrinho(id, quantidade)
        .subscribe({
          next: response => {
            const resp = (<any>response);
            console.log(resp);
            this.router.navigate(["carrinho"]);
          }, error: e => {
            const errMsg = e;
            console.log(errMsg);
          }
        })
  }

  excluirProduto(id: any) {
    console.log("Id: " + id);
    this.produtoService.excluirProduto(id)
      .subscribe({
        next: response => {
          const resp = (<any>response);
          console.log(resp);
          window.location.reload();
        }, error: e => {
          const errMsg = e;
          console.log(errMsg)
        }
      })
  }

  desabilitarBotao(quantidade: string) {
    if(quantidade == '0')
      return false;
    return true;
  }

}

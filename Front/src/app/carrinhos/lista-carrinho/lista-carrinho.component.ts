import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Carrinho } from '../carrinho';
import { CarrinhoService } from '../carrinho.service';
import { ItemCarrinho } from '../itemCarrinho';

@Component({
  selector: 'app-lista-carrinho',
  templateUrl: './lista-carrinho.component.html'
})
export class ListaCarrinhoComponent implements OnInit {

  public itensCarrinho!: ItemCarrinho[];
  public valorCompra!: Carrinho;
  constructor(private carrinhoService: CarrinhoService, private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    this.carrinhoService.pegarItensCarrinho()
      .subscribe({
        next: itens => {
        this.itensCarrinho = itens;
        console.log(this.itensCarrinho)
        }, error: e => {
          const errMsg = e;
          console.log(errMsg)
        }
      })

      this.carrinhoService.pegarValorCarrinho()
        .subscribe({
          next: carrinho => {
          this.valorCompra = carrinho;
          }, error: e => {
            const errMsg = e;
            console.log(errMsg)
          }
        })
  }

  excluirProduto(id: number) {
    this.carrinhoService.excluirItemCarrinho(id)
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

  irParaEndereco() {
    this.router.navigate(["enderecoenvio"]);
  }
  
}

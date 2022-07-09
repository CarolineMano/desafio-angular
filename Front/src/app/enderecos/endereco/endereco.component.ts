import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CarrinhoService } from 'src/app/carrinhos/carrinho.service';
import { Endereco } from '../endereco';
import { EnderecoService } from '../endereco.service';

@Component({
  selector: 'app-endereco',
  templateUrl: './endereco.component.html'
})
export class EnderecoComponent implements OnInit {

  constructor(private enderecoService: EnderecoService, private carrinhoService: CarrinhoService, private router: Router, private http: HttpClient) { }
  public enderecoEnvio: Endereco = new Endereco;
  public enderecoValido!: boolean;
  public cepValido!: boolean;
  public cep: string = "";

  ngOnInit(): void {
    this.enderecoValido = false;
    this.cepValido = true;
  }

  buscarCep(cep: string) {
    this.enderecoService.pegarEndereco(cep)
      .subscribe({
        next: endereco => {
          this.enderecoEnvio = endereco;
          this.enderecoValido = true;
          this.cepValido = true;
          console.log(this.enderecoEnvio.state)
        }, error: e => {
          const errMsg = e;
          console.log(errMsg);
          this.enderecoValido = false;
          this.cepValido = false;
        }
      })
  }

  finalizarCompra() {
    const endereco = JSON.stringify(this.enderecoEnvio);
    this.carrinhoService.comprarCarrinho(endereco)
    .subscribe({
      next: response => {
        const resp = (<any>response);
        this.router.navigate(['minhascompras'])
      }, error: e => {
        const errMsg = e;
        console.log(errMsg);
      }
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { Carrinho } from '../carrinho';
import { CarrinhoService } from '../carrinho.service';

@Component({
  selector: 'app-carrinho-finalizado',
  templateUrl: './carrinho-finalizado.component.html'
})
export class CarrinhoFinalizadoComponent implements OnInit {

  constructor(private carrinhoService: CarrinhoService) { }
  public carrinhos!: Carrinho[]

  ngOnInit(): void {
    this.carrinhoService.pegarCarrinhosFinalizados()
      .subscribe({
        next: carrinhos => {
        this.carrinhos = carrinhos;
        console.log(this.carrinhos)
        }, error: e => {
          const errMsg = e;
          console.log(errMsg)
        }
      })
  }

}

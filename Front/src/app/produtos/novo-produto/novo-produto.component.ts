import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorService } from 'src/app/error/error.service';
import { ErrorResponse } from 'src/app/error/errorResponse';
import { ProdutoService } from '../produto.service';
import { ProdutoDto } from '../produtoDto';

@Component({
  selector: 'app-novo-produto',
  templateUrl: './novo-produto.component.html'
})
export class NovoProdutoComponent implements OnInit {

  constructor(private produtoService: ProdutoService, private errorService: ErrorService, private router: Router) { }
  public novoProduto!: ProdutoDto;
  errorString: any;
  existeErro!: boolean;


  ngOnInit(): void {
    this.novoProduto = new ProdutoDto;
  }

  fileUpload($event: any) {
    this.novoProduto.imagem = $event.target.files[0];
  }

  adicionarProduto() {
    this.novoProduto.precoUnitario = this.novoProduto.precoUnitario.toString().replace('.', ',');
    let produto = new FormData();
    produto.append('Marca', this.novoProduto.marca);
    produto.append('Descricao', this.novoProduto.descricao);
    produto.append('UnidadeDeMedida',this.novoProduto.unidadeDeMedida);    
    produto.append('PrecoUnitario', this.novoProduto.precoUnitario);
    produto.append('QuantidadeEstoque',this.novoProduto.quantidadeEstoque);
    produto.append('Imagem', this.novoProduto.imagem);

    this.produtoService.adicionarProduto(produto)
      .subscribe({
        next: response => {
          const resp = (<any>response);
          console.log(resp)
          this.router.navigate(["produtos"])
        }, error: e => {
          const errMsg = e as ErrorResponse;
          this.errorString = errMsg.error;
          console.log(this.errorString)
          this.errorString = this.errorService.stringifyError(this.errorString)
          this.existeErro = true;
        }
      })
  }

  habilitarBotao() {
    if(this.novoProduto.marca &&
      this.novoProduto.descricao &&
      this.novoProduto.precoUnitario &&
      this.novoProduto.quantidadeEstoque &&
      this.novoProduto.unidadeDeMedida &&
      this.novoProduto.imagem)
      return false;
    else  
      return true;
  }
}

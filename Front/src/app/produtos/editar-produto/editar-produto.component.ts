import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Produto } from '../produto';
import { ProdutoService } from '../produto.service';

@Component({
  selector: 'app-editar-produto',
  templateUrl: './editar-produto.component.html'
})
export class EditarProdutoComponent implements OnInit {

  public routeId!: string;
  public produtoBd!: Produto;
  public imagem!: File;

  constructor(private currentRoute: ActivatedRoute, private produtoService: ProdutoService, private router: Router) { }

  ngOnInit(): void {
    this.routeId = this.currentRoute.snapshot.params['id'];

    this.produtoService.pegarProduto(this.routeId)
      .subscribe({
        next: response => {
          this.produtoBd = response;
        }, error: e => {
          const errMsg = e;
          console.log(errMsg);
        }
      })
  }

  fileUpload($event: any) {
    this.imagem = $event.target.files[0];
  }

  editarProduto(id: any) {
    this.produtoBd.precoUnitario = this.produtoBd.precoUnitario.toString().replace('.', ',');
    let produto = new FormData();

    produto.append('Marca', this.produtoBd.marca);
    produto.append('Descricao', this.produtoBd.descricao);
    produto.append('UnidadeDeMedida',this.produtoBd.unidadeDeMedida);    
    produto.append('PrecoUnitario', this.produtoBd.precoUnitario);
    produto.append('QuantidadeEstoque',this.produtoBd.quantidadeEstoque);
    produto.append('Imagem', this.imagem);

    this.produtoService.editarProduto(this.routeId, produto)
    .subscribe({
      next: response => {
        const resp = (<any>response);
        console.log(resp)
        this.router.navigate(["produtos"])
      }, error: e => {
        const errMsg = e;
        console.log(errMsg);
      }
    })
  }
}



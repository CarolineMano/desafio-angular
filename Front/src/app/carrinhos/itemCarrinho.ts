import { Produto } from "../produtos/produto";

export class ItemCarrinho {
    id!: number;
    carrinhoId!: number;
    produtoId!: number;
    produto!: Produto;
    quantidadeProduto!: number;
    valorTotalItem!: string
}
import { Routes, CanActivate } from "@angular/router";
import { ListaCarrinhoComponent } from "./carrinhos/lista-carrinho/lista-carrinho.component";
import { EnderecoComponent } from "./enderecos/endereco/endereco.component";
import { LoginComponent } from "./login/login.component";
import { HomeComponent } from "./navegacao/home/home.component";
import { EditarProdutoComponent } from "./produtos/editar-produto/editar-produto.component";
import { ListaProdutoComponent } from "./produtos/lista-produto/lista-produto.component";
import { NovoProdutoComponent } from "./produtos/novo-produto/novo-produto.component";
import { RegistroComponent } from "./registro/registro.component";
import { AdminGuardService } from "./guards/admin-guard.service";
import { ComumGuardService } from "./guards/comum-guard.service";
import { CarrinhoFinalizadoComponent } from "./carrinhos/carrinho-finalizado/carrinho-finalizado.component";

export const rootRouterConfig: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full'},
    { path: 'home', component: HomeComponent},
    { path: 'login', component: LoginComponent},
    { path: 'novaconta', component: RegistroComponent},
    { path: 'produtos', component: ListaProdutoComponent},
    { path: 'carrinho', canActivate: [ComumGuardService], component: ListaCarrinhoComponent},
    { path: 'enderecoenvio', canActivate: [ComumGuardService], component: EnderecoComponent},
    { path: 'novoproduto', canActivate: [AdminGuardService], component: NovoProdutoComponent},
    { path: 'editarproduto/:id', canActivate: [AdminGuardService], component: EditarProdutoComponent},
    { path: 'minhascompras', canActivate: [ComumGuardService], component: CarrinhoFinalizadoComponent}
]
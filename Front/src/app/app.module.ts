import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxMaskModule } from 'ngx-mask';

import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
registerLocaleData(localePt);

import { AppComponent } from './app.component';
import { MenuComponent } from './navegacao/menu/menu.component';
import { FooterComponent } from './navegacao/footer/footer.component';
import { rootRouterConfig } from './app.route';
import { HomeComponent } from './navegacao/home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistroComponent } from './registro/registro.component';
import { ListaProdutoComponent } from './produtos/lista-produto/lista-produto.component';
import { ListaCarrinhoComponent } from './carrinhos/lista-carrinho/lista-carrinho.component';
import { EnderecoComponent } from './enderecos/endereco/endereco.component';
import { NovoProdutoComponent } from './produtos/novo-produto/novo-produto.component';
import { EditarProdutoComponent } from './produtos/editar-produto/editar-produto.component';

import { ProdutoService } from './produtos/produto.service';
import { CarrinhoService } from './carrinhos/carrinho.service';
import { EnderecoService } from './enderecos/endereco.service';
import { AdminGuardService } from './guards/admin-guard.service';
import { ComumGuardService } from './guards/comum-guard.service';
import { ErrorService } from './error/error.service';
import { CarrinhoFinalizadoComponent } from './carrinhos/carrinho-finalizado/carrinho-finalizado.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    RegistroComponent,
    ListaProdutoComponent,
    ListaCarrinhoComponent,
    EnderecoComponent,
    NovoProdutoComponent,
    EditarProdutoComponent,
    CarrinhoFinalizadoComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    [RouterModule.forRoot(rootRouterConfig, { useHash: false})],
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    }),
    NgxMaskModule.forRoot()    
  ],
  providers: [
    ProdutoService,
    CarrinhoService,
    EnderecoService,
    AdminGuardService,
    ComumGuardService,
    ErrorService,
    {provide: APP_BASE_HREF, useValue: '/'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

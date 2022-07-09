<h1 align="center">Desafio Angular</h1> 

<p style="text-align: justify">Implementação de uma API de e-commerce de produtos com .NET e seu respectivo front-end com Angular, consistindo em um carrinho de compras com controle de rotas, autenticação de usuário (JWT Token) e imagens do produto.</p>

## :bomb: Descrição do Desafio

<p style="text-align: justify">As funcionalidades obrigatórias para o desafio são: </p>

- Cadastro do Usuário;
- Autenticação do Usuário;
- Cadastro do Produto;
- Carrinho de Compra / Finalizar Compra

**<h4>Obrigatório</h4>**

- Os cadastros e os dados relacionados às vendas/carrinho de compras deveriam ser armazenados em banco de dados via API, por isso foi necessário desenvolver a aplicação back-end.

- Para acessar as páginas das funcionalidades propostas (exceto a de autenticação), o usuário deve estar logado. 

- O CPF do usuário deve ser validado via API (api.invertexto.com).

**<h4>Exceeds</h4>**

- Cadastro do endereço para o onde o pedido será enviado, neste caso, recuperando o endereço via API (api.invertexto.com).

**<h4>Entidades</h4>**

- Usuário
    - Identificação (CPF)
    - Nome
    - E-mail
    - Senha

- Produto
    - Id
    - Descrição
    - Unidade de Medida
    - Foto
    - Preço Unitário
    - Quantidade em Estoque

- Carrinho de Compra
    - Id
    - Data da Venda
    - Id do usuário que comprou
    - Valor Total da Venda
    - CEP para onde foi enviado

- Item de Compra
    - Id
    - Id da Compra
    - Id do Produto
    - Quantidade do Produto
    - Valor Unitário do Produto
    - Valor Total do Item

---

# :memo: Descrição dos Programas

## :factory: API

<p style="text-align: justify">Possui três controladores: Conta, MeuCarrinho e Produto. </p>

**<h4>Endpoints</h4>**

- Conta: 
    - Registrar: responsável por realizar o registro de um novo usuário comum no sistema.
    - Login: realiza login de usuário previamente cadastrado no sistema.

- Meu Carrinho: 
    - Itens: retorna a relação de todos os itens existentes no carrinho aberto do usuário logado.
    - MeuCarrinho: retorna o carrinho aberto do usuário logado.
    - AdicionarProduto: adiciona um novo produto ao carrinho do usuário. Se nenhum carrinho estiver aberto, abrirá um novo.
    - ExcluirProduto: exclui um produto do carrinho aberto do usuário logado.
    - Comprar: finaliza a compra, salvando na entidade carrinho os dados das compras, inclusive endereço completo de entrega.
    - Todos: pega todos os carrinhos(compras) finalizados.

- Produto:
    - Produto(get): pega todos os produtos ativos cadastrados na API. Aberto inclusive para usuários anônimos.
    - Produto(post): adiciona novo produto ao banco de dados, disponível apenas para admin.
    - Produto(get: id): retorna um produto por id. Disponível para usuários anônimos.
    - Produto(patch: id): edita um produto já existente. Disponível apenas para admin.
    - Produto(delete: id): deleta um produto já existente. Disponível apenas para admin.

## :european_castle: Front-end

**<h4>Rotas</h4>**

- Home: página inicial da aplicação, com uma mensagem simples.
- Login: formulário de login de usuário já cadastrado. Responsável por validar e salvar algumas informações no LocalStorage, como o token, role e nome do usuário.
- NovaConta: formulário para registro de novo usuário ao sistema. 
- VerProdutos: página que irá mostrar todos os produtos ativos. Irá apresentar diferenças a depender do tipo do usuário (comum, admin ou anônimo). Um usuário anônimo poderá apenas visualizar os produtos, um comum poderá adicioná-los ao seu carrinho e o admin poderá adicionar novos produtos e editar ou excluir produtos existentes.
- Carrinho: rota disponível apenas para usuários comuns, irá mostrar os produtos existentes no carrinho. Apresentará as opções de excluir itens do carrinho e finalizar a compra, sendo que esta última opção irá levar o usuário à página de endereço de envio.
- EnderecoEnvio: página final para a finalização da compra, o usuário deverá digitar um CEP para que o endereço seja buscado automaticamente. Então ele deverá completar com o número da casa e finalizar a compra.
- NovoProduto: o admin cadastrado poderá cadastrar um novo produto através do formulário. O botão para finalizar a adição só será ativado após todos os campos serem preenchidos.
- EditarProduto/id: o admin poderá editar um produto que conste da lista de produtos. Todos os dados serão preenchidos automaticamente neste formulário e o admin poderá realizar as alterações que quiser. Não é necessário fazer upload de nova imagem.
- MinhasCompras: retorna uma lista de todas as compras finalizadas pelo usuário logado. Nesta lista não entra o carrinho aberto.  

---

## :floppy_disk: Banco de Dados

<p style="text-align: justify">MySql foi configurado na porta 3306, com uid e senha iguais a root. Será criado automaticamente o banco de nome desafio_angular_mano assim que a API for iniciada pela primeira vez.

Serão criados automaticamente dois produtos e dois usuários, um admin e outro usuário comum. </p>

<p style="text-align: justify">Os dados de login dos usuários criados automaticamente são os seguintes: </p>

Perfil | Usuário | Senha |
------|---------|------|
Admin | admin@email.com | Gft2022 |
Comum | joel@email.com | Gft2022 |

---

## :loudspeaker: Informações importantes

<p style ="text-align: justify">Os dois projetos foram feitos separadamente, desta forma, cada um deverá ser rodado de forma autônoma para que funcionem.</p>

<p style ="text-align: justify">Além disso, pode ser necessário configurar o certificado HTTPS para que seja confiável. Para isto, basta utilizar o seguinte comando na API: </p>

> dotnet dev-certs https --trust

<p style ="text-align: justify">Para rodar a aplicação Angular, será necessário instalar os node_modules: </p>

> npm install

---

## :arrow_heading_up: Melhorias futuras

<p style ="text-align: justify">Melhorar a tratativa de erros da API no front-end e melhorar o design das páginas.</p>

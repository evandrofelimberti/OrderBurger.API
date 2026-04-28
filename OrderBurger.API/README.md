# OrderBurger.API

API RESTful para gerenciamento de **cardápio (produtos)** e **pedidos**, desenvolvida com **ASP.NET Core (.NET 9)**.

## Tecnologias
- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- AutoMapper
- FluentValidation
- Swagger/OpenAPI

---

## Como executar

### Pré-requisitos
- SDK .NET 9 instalado

### Passos
1. Restaurar dependências
   ```bash
   dotnet restore
   ```
2. Executar o projeto
   ```bash
   dotnet run --project OrderBurger.API
   ```
3. Acessar Swagger:
   - `https://localhost:{porta}/` (em ambiente de desenvolvimento)

---

## Endpoints principais

Base: `api/v1`

## Produtos (Cardápio)

### 1) Listar cardápio
**GET** `/api/v1/Product`

Retorna todos os produtos cadastrados.

### 2) Consultar produto por ID
**GET** `/api/v1/Product/{id}`

Retorna os detalhes de um produto específico.

### 3) Cadastrar produto
**POST** `/api/v1/Product`

Exemplo de body:
json { "code": "X-BURGER-01", "description": "Hambúrguer clássico", "name": "X-Burger", "price": 24.9, "category": 1 }


### 4) Atualizar produto
**PUT** `/api/v1/Product/{id}`

Atualiza os dados de um produto existente.

### 5) Remover produto
**DELETE** `/api/v1/Product/{id}`

Remove um produto do cardápio.

---

## Pedidos

### 1) Criar pedido
**POST** `/api/v1/Orders`

Exemplo de body:
json { "consumerName": "João", "items": [{ "productId": "GUID_DO_PRODUTO", "quantity": 1 }]}


### 2) Listar pedidos
**GET** `/api/v1/Orders`

Retorna todos os pedidos.

### 3) Consultar pedido por ID
**GET** `/api/v1/Orders/{id}`

Retorna os detalhes de um pedido específico.

### 4) Adicionar item ao pedido
**POST** `/api/v1/Orders/{orderId}/items`

Exemplo de body:
json { "productId": "GUID_DO_PRODUTO", "quantity": 1 }


### 5) Remover item do pedido
**DELETE** `/api/v1/Orders/{orderId}/items/{productId}`

Remove um item do pedido pelo `productId`.

---

## Validações e tratamento de erro

A API utiliza **FluentValidation** para validar os DTOs de entrada.

## Exemplos de validação:
- Produto:
   - Código obrigatório
   - Nome obrigatório
   - Descrição obrigatória
   - Preço deve ser maior que zero
- Pedido:
   - Deve conter ao menos 1 item
   - **Não permite produtos duplicados** no mesmo pedido
- Item do pedido:
   - `ProductId` obrigatório
   - `Quantity` maior que zero

### Exemplo de erro: produtos duplicados
Se o mesmo `productId` for enviado mais de uma vez em `items` no `POST /Orders`, a API retorna erro de validação com mensagem:

`"O pedido contém produtos duplicados"`

---

## Observações
- Use o Swagger para testar os endpoints e visualizar contratos.
- Para criar pedido, os produtos referenciados precisam existir no cardápio.
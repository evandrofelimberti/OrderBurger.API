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
- xUnit

---

## Arquitetura (visão rápida)

Estrutura organizada por camadas:
- `Controllers` (entrada HTTP)
- `Services` (regras de negócio)
- `Repositories` (acesso a dados)
- `Models` (entidades)
- `DTOs` e `Mappings` (contratos e mapeamentos)
- `Validators` (validação de entrada)

---

## Como executar

### Pré-requisitos
- SDK .NET 9 instalado
- Ferramenta EF Core instalada (caso não tenha):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Passos
1. Restaurar dependências:
   ```bash
   dotnet restore
   ```

2. Aplicar as migrations no banco:
   ```bash
   dotnet ef migrations add CreatDatabase 

   dotnet ef database update --project OrderBurger.API --startup-project OrderBurger.API
   ```

3. Executar a API:
   ```bash
   dotnet run --project OrderBurger.API
   ```

4. Acessar o Swagger (ambiente Development):
    - `https://localhost:{porta}/`

> Em ambiente de desenvolvimento, a aplicação aplica migrations automaticamente na inicialização.

---

## Executar testes

### Testes unitários + integração
bash dotnet test

---

## Endpoints principais

Base: `api/v1`

## Produtos (Cardápio)

### 1) Listar cardápio
**GET** `/api/v1/product`

### 2) Consultar produto por ID
**GET** `/api/v1/product/{id}`

### 3) Cadastrar produto
**POST** `/api/v1/product`

Exemplo de body:
json:
```bash
 {
    "code": "X-BURGER-01",
    "description": "Hambúrguer clássico",
    "name": "X-Burger",
    "price": 24.90,
    "category": 1
}
```

### 4) Atualizar produto
**PUT** `/api/v1/product/{id}`

### 5) Remover produto
**DELETE** `/api/v1/product/{id}`

---

## Pedidos

### 1) Criar pedido
**POST** `/api/v1/orders`

Exemplo de body (json):
```bash
{
    "customerName": "João",
    "items":
}
```


### 2) Listar pedidos
**GET** `/api/v1/orders`

### 3) Consultar pedido por ID
**GET** `/api/v1/orders/{id}`

### 4) Adicionar item ao pedido
**POST** `/api/v1/orders/{orderId}/items`

Exemplo de body (json):
```bash
{
    "productId": "GUID_DO_PRODUTO",
    "quantity": 1
}
```


### 5) Remover item do pedido
**DELETE** `/api/v1/orders/{orderId}/items/{productId}`

---

## Regras de validação e negócio

A API utiliza **FluentValidation** para validar DTOs de entrada e **Services** para validar regras de negócio.

### Produto
- Código obrigatório
- Nome obrigatório
- Descrição obrigatória
- Preço deve ser maior que zero

### Pedido (entrada)
- Deve conter ao menos 1 item
- Não permite `productId` duplicado na lista de itens

### Pedido (negócio)
- Produto deve existir
- Não permite **categoria duplicada** no mesmo pedido:
    - ao criar pedido com lista de itens
    - ao adicionar novo item em pedido existente

### Item do pedido
- `ProductId` obrigatório
- `Quantity` deve ser igual a `1`

---

## Erros esperados (exemplos)

- Produto não encontrado no cardápio
- Produto duplicado por ID na requisição de criação
- Categoria duplicada no pedido

> Consulte o Swagger para ver os contratos e os códigos de resposta retornados por cada endpoint.

---

## Observações
- Para criar/editar pedidos, os produtos referenciados precisam existir no cardápio.
- O projeto possui testes de integração para os controllers principais (`Product` e `Orders`) validando status code e contrato JSON.
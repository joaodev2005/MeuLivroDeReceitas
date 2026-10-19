# MyRecipeBook — API de Receitas em .NET

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=fff&style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=fff&style=for-the-badge)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff&style=for-the-badge)

> **Status:** 🚧 em desenvolvimento — projeto de estudo.

## Sobre

API REST para gerenciamento de receitas culinárias, construída em **.NET** com **C#**.
O objetivo deste repositório é praticar, na prática, o desenvolvimento de uma API
completa: arquitetura em camadas, autenticação, persistência, testes automatizados
e pipeline de entrega.

## Tecnologias

- **.NET 10** / C#
- **ASP.NET Core** (Minimal APIs / Controllers)
- **Entity Framework Core** + **FluentMigrator**
- **SQL Server** (e MySQL, conforme o andamento do curso)
- **JWT** + Refresh Token, hashing com **Argon2**
- **xUnit** + **Shouldly** para testes
- **Docker** para o ambiente de desenvolvimento
- **Azure DevOps** para CI/CD

## Estrutura da solução

```txt
src/
 ├── Backend/
 │   ├── Api/
 │   ├── Application/
 │   ├── Domain/
 │   └── Infrastructure/
 │
 └── Shared/
     ├── Communication/
     └── Exception/

tests/
 ├── UseCases.Tests/
 ├── Validators.Tests/
 └── WebApi.Tests/
```

- **Api** — endpoints, middlewares, autenticação e Swagger.
- **Application** — casos de uso e regras de negócio.
- **Domain** — entidades e contratos.
- **Infrastructure** — banco de dados, migrations, autenticação, integrações.
- **Communication** — requests/responses trocados com a API.
- **Exception** — exceções e tratamento de erros centralizado.

## Como executar

### Requisitos

- .NET SDK 10
- SQL Server (ou MySQL, conforme configuração)
- Docker Desktop
- Git

### Passos

```sh
git clone https://github.com/<seu-usuario>/MyRecipeBook.git
cd MyRecipeBook
dotnet restore
dotnet run --project src/Backend/Api
```

Configure o `appsettings.Development.json` com a string de conexão e as chaves de JWT:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=meulivrodereceitas;User Id=sa;Password=sua_senha;"
  },
  "Jwt": {
    "SigningKey": "sua-chave-secreta-com-no-minimo-32-caracteres",
    "ExpirationTimeMinutes": 1000
  }
}
```

Swagger disponível em `https://localhost:7070/swagger` (a porta depende do `launchSettings.json`).

## Testes

```sh
dotnet test
```

Os testes de integração exigem o Docker em execução.

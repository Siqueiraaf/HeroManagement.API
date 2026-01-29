# 🦸 Hero Management System

Sistema de gerenciamento de super-heróis desenvolvido com **.NET 8 (ASP.NET Core + EF Core)** no backend e **Angular 16** no frontend, aplicando **DDD (Domain-Driven Design)** e **Clean Code**.

---

## Objetivo do Projeto

O objetivo deste projeto é demonstrar:

- Criação de uma **API CRUD** de super-heróis
- Relacionamento **muitos-para-muitos** entre Heróis e Superpoderes
- Aplicação prática de **DDD**
- Código limpo, desacoplado e testável
- Integração frontend (Angular) com backend (.NET)

---

## Arquitetura Geral

```
HeroManagement
│
├── Backend
│ ├── HeroManagement.API
│ ├── HeroManagement.Application
│ ├── HeroManagement.Domain
│ └── HeroManagement.Infrastructure
│
└── Frontend
└── hero-management-frontend
```

## Por que usei DDD (Domain-Driven Design)?
Utilizei DDD para separar responsabilidades, proteger regras de negócio e garantir escalabilidade.

Benefícios do DDD neste projeto
- Código desacoplado
- Facilidade para manutenção
- Clareza nas regras de negócio
- Facilidade para testes (não realizei unitários devido ao prazo)
- Evolução segura do sistema, pois sabia que iria ter muitas possibilidades.

## Por que Clean Code?
Clean Code foi aplicado para garantir:

- Métodos pequenos e objetivos
- Nomes claros e sem ambiguidade
- Baixo acoplamento
- Alta legibilidade
- Facilidade de manutenção

### Exemplos que apliquei:
- DTOs específicos para cada operação
- Serviços com responsabilidade única
- Separação clara entre leitura e escrita
- Evita lógica de negócio em Controllers

## Camadas
Contém:
- Entidades (`Heroi`, `Superpoder`)
- Regras de negócio
- Interfaces (contratos)

Não depende de nenhuma outra camada

## Application
Contém:
- Casos de uso
- DTOs (`CriarHeroiDto`, `AtualizarHeroiDto`)
- Serviços de aplicação (`IHeroiService`)

Orquestra o domínio  
Não acessa diretamente banco de dados 

## Infrastructure
Contém:
- EF Core
- DbContext
- Repositórios
- Mapeamentos de banco

Implementa contratos do domínio

## API
Contém:
- Controllers
- Configuração de DI
- Endpoints HTTP

Camada mais externa  
Não contém regra de negócio  

## Banco de Dados
Escolhi o SQL Server e deixo aqui o script da criação do banco conforme a documentação.
```sql
CREATE TABLE Superpoderes (
    Id INT IDENTITY PRIMARY KEY,
    Superpoder NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(250)
);

CREATE TABLE Herois (
    Id INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(120) NOT NULL,
    NomeHeroi NVARCHAR(120) NOT NULL,
    DataNascimento DATETIME2 NOT NULL,
    Altura FLOAT NOT NULL,
    Peso FLOAT NOT NULL
);

CREATE TABLE HeroisSuperpoderes (
    HeroiId INT NOT NULL,
    SuperpoderId INT NOT NULL,
    CONSTRAINT PK_HeroisSuperpoderes PRIMARY KEY (HeroiId, SuperpoderId),
    CONSTRAINT FK_Herois FOREIGN KEY (HeroiId) REFERENCES Herois(Id),
    CONSTRAINT FK_Superpoderes FOREIGN KEY (SuperpoderId) REFERENCES Superpoderes(Id)
);
```

## Endpoints da API
Base URL: `https://localhost:7052/api/herois`

| Método | Endpoint         | Descrição           |
| ------ | ---------------- | ------------------- |
| POST   | `/create`         | Criar herói         |
| GET    | `/search`        | Listar heróis       |
| GET    | `/search/{id}`    | Buscar herói por ID |
| PUT    | `/update/{id}` | Atualizar herói     |
| DELETE | `/delete/{id}`   | Deletar herói       |


## Frontend (Angular 16)
Funcionalidades:
- Criar herói
- Listar heróis
- Buscar herói por ID
- Atualizar herói
- Deletar herói
- Selecionar múltiplos superpoderes

Tecnologias:
- Angular 16
- Reactive Forms
- Standalone Components
- HttpClient

## Como executar o projeto:
Backend:

    cd Backend/HeroManagement.API
    dotnet restore
    dotnet run

Acesse: `https://localhost:7052/index.html`

Frontend:

    cd Frontend/hero-management-frontend
    npm install
    ng serve

Acesse:`http://localhost:4200`

### Exemplo Criação de super heroi via Swagger:
```
{
  "nome": "Bruce Wayne",
  "nomeHeroi": "Batman",
  "dataNascimento": "1985-02-19",
  "altura": 1.88,
  "peso": 95,
  "superpoderesIds": [1, 3]
}


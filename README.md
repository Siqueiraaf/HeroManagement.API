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
Escolhi o SQL Server aqui consta 2 scripts da criação do banco. Utilizei um deles com mock.
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
```
-- =============================================
-- Scripts de Criação das Tabelas - Super Heróis
-- Banco de Dados: SQL Server
-- =============================================

-- Criação do banco de dados (opcional)
-- CREATE DATABASE SuperHeroesDB;
-- GO
-- USE SuperHeroesDB;
-- GO

-- Tabela Superpoderes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Superpoderes')
BEGIN
    CREATE TABLE Superpoderes (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Superpoder NVARCHAR(50) NOT NULL,
        Descricao NVARCHAR(250) NULL
    );
END
GO

-- Tabela Herois
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Herois')
BEGIN
    CREATE TABLE Herois (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nome NVARCHAR(120) NOT NULL,
        NomeHeroi NVARCHAR(120) NOT NULL,
        DataNascimento DATETIME2(7) NOT NULL,
        Altura FLOAT NOT NULL,
        Peso FLOAT NOT NULL
    );
END
GO

-- Tabela HeroisSuperpoderes (Relacionamento N-N)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeroisSuperpoderes')
BEGIN
    CREATE TABLE HeroisSuperpoderes (
        HeroiId INT NOT NULL,
        SuperpoderId INT NOT NULL,
        CONSTRAINT PK_HeroisSuperpoderes PRIMARY KEY (HeroiId, SuperpoderId),
        CONSTRAINT FK_HeroisSuperpoderes_Herois FOREIGN KEY (HeroiId) 
            REFERENCES Herois(Id) ON DELETE CASCADE,
        CONSTRAINT FK_HeroisSuperpoderes_Superpoderes FOREIGN KEY (SuperpoderId) 
            REFERENCES Superpoderes(Id) ON DELETE CASCADE
    );
END
GO

-- Inserção de Superpoderes iniciais (opcional)
IF NOT EXISTS (SELECT * FROM Superpoderes)
BEGIN
    INSERT INTO Superpoderes (Superpoder, Descricao) VALUES
    ('Super Força', 'Capacidade de levantar objetos extremamente pesados'),
    ('Voo', 'Capacidade de voar e levitar'),
    ('Velocidade', 'Capacidade de se mover em velocidades sobre-humanas'),
    ('Invisibilidade', 'Capacidade de se tornar invisível'),
    ('Telepatia', 'Capacidade de ler mentes'),
    ('Regeneração', 'Capacidade de curar ferimentos rapidamente'),
    ('Controle de Fogo', 'Capacidade de criar e controlar o fogo'),
    ('Controle de Gelo', 'Capacidade de criar e controlar o gelo'),
    ('Raios Laser', 'Capacidade de emitir raios de energia pelos olhos'),
    ('Elasticidade', 'Capacidade de esticar o corpo'),
    ('Telecinese', 'Capacidade de mover objetos com a mente'),
    ('Super Inteligência', 'Inteligência muito acima da média humana');
END
GO

-- Índices para otimização de consultas
CREATE NONCLUSTERED INDEX IX_Herois_Nome ON Herois(Nome);
CREATE NONCLUSTERED INDEX IX_Herois_NomeHeroi ON Herois(NomeHeroi);
GO

PRINT 'Tabelas criadas com sucesso!';
GO
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


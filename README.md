🍽 Gordinhos Felizes

Plataforma de avaliação de restaurantes desenvolvida em C# com ASP.NET Core, projetada para demonstrar boas práticas de desenvolvimento backend utilizando SOLID, Clean Architecture e API REST.

O sistema permite cadastrar restaurantes, registrar avaliações e gerar rankings baseados na média das notas recebidas.

Este projeto foi desenvolvido como parte de um portfólio de desenvolvimento backend, com foco em arquitetura de software, organização de código e integração com serviços em nuvem.

📌 Funcionalidades

Cadastro de restaurantes

Avaliação de restaurantes por administradores

Cálculo automático da média de avaliações

Listagem de restaurantes

Ranking dos restaurantes mais bem avaliados

API REST para acesso aos dados

Estrutura baseada em Clean Architecture

🏗 Arquitetura do Sistema

A aplicação segue o padrão Clean Architecture, separando responsabilidades em diferentes camadas.

Fluxo da aplicação:

Frontend (GitHub Pages)
        ↓
ASP.NET Core API
        ↓
Banco de Dados no Azure

Camadas da aplicação:

src
│
├ GordinhosFelizes.API
│
├ GordinhosFelizes.Application
│
├ GordinhosFelizes.Domain
│
└ GordinhosFelizes.Infrastructure

Descrição das camadas:

Camada	Responsabilidade
Domain	Entidades e regras de negócio
Application	Casos de uso e serviços
Infrastructure	Acesso a dados e integrações
API	Controllers e endpoints
🧰 Tecnologias Utilizadas

C#

ASP.NET Core

Entity Framework Core

PostgreSQL ou SQL Server

Microsoft Azure

GitHub Pages

🗄 Modelagem do Banco de Dados
Restaurants
Campo	Tipo
Id	int
Name	string
Address	string
City	string
CreatedAt	datetime
Users
Campo	Tipo
Id	int
Name	string
Email	string
PasswordHash	string
IsAdmin	bool
CreatedAt	datetime
Reviews
Campo	Tipo
Id	int
RestaurantId	int
UserId	int
Rating	int
Comment	string
CreatedAt	datetime

Relacionamentos:

Users 1 ---- N Reviews

Restaurants 1 ---- N Reviews
🌐 Endpoints da API
Restaurantes
Listar restaurantes
GET /restaurants
Buscar restaurante por ID
GET /restaurants/{id}
Criar restaurante
POST /restaurants
Avaliações
Criar avaliação
POST /reviews
Listar avaliações de um restaurante
GET /restaurants/{id}/reviews
Ranking
Restaurantes mais bem avaliados
GET /restaurants/top
🔐 Autenticação

O sistema possui autenticação para administradores utilizando JSON Web Token.

Somente administradores podem:

cadastrar restaurantes

registrar avaliações

Administradores iniciais do sistema:

Edson

Mari

🎨 Estrutura do Frontend

O frontend é uma aplicação estática hospedada no GitHub Pages.

Estrutura:

frontend
│
├ index.html
├ restaurants.html
├ restaurant.html
├ admin.html
│
├ css
│  └ style.css
│
└ js
   ├ api.js
   ├ restaurants.js
   └ reviews.js
🚀 Deploy

Arquitetura de deploy:

GitHub Pages (Frontend)
        ↓
Azure App Service (API)
        ↓
Azure Database (PostgreSQL / SQL Server)

Serviços utilizados:

Azure App Service

Azure Database for PostgreSQL

🧪 Testes

O projeto inclui testes unitários utilizando:

xUnit

Testes focados em:

regras de negócio

services da aplicação

validações

🧾 Padrão de Commits

Este projeto utiliza Conventional Commits.

Exemplos:

feat: create restaurant entity
feat: implement restaurant service
feat: add restaurant controller
fix: correct rating calculation
docs: update project README
📅 Roadmap do Projeto
Versão 1

Cadastro de restaurantes

Avaliações

Média de avaliações

API REST

Versão 2

Ranking de restaurantes

Filtro por categoria

Busca de restaurantes

Versão 3

Upload de fotos

Dashboard admin

Mapa de restaurantes

💡 Melhorias Futuras

Sistema de favoritos

Aplicação mobile

Geolocalização de restaurantes

Recomendações baseadas em avaliações

👨‍💻 Autor

Desenvolvido por Edson Elias Miziara Filho

GitHub

LinkedIn

Portfólio

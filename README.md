# Code Challenge Grupo ZAP

O seguinte projeto faz parte do processo seletivo da OLX/Zap+. 

-Opção B: Fazer uma API (backend): O projeto consiste em uma API REST, onde dada a origem do portal em uma request (Zap | Viva Real) o seu response será a listagem dos imóveis.

## Decisões técnicas

- API foi implementada com o .NET 5;
- API REST com arquitetura em camadas baseada nos princípios do DDD e testes unitários;
- Foi utilizada o Swagger - OpenApi para documentação e interface prática dos endpoints;
- Outra tecnologias: Serilog, AutoMapper, FluentValidation, FluentAssertion, Moq, xUnit, Docker, Heroku, etc.
- Trello para planejamento das atividades: https://trello.com/b/W33srlBq/desafio-olx-eng-zap-challenge

## Arquitetura

## Endpoints da API

#### 1 - Carga de imóveis em memória
- Carrega os imóveis na memória da aplicação a partir de uma url com o arquivo de source aplicando as regras de negócio para imóveis elegíveis:
```
POST /realestates/load
```
- É necessário executar esse comando primeiro informando o endereço do source no corpo da requisição:
```
POST /realestates/load
{
   "url": "http://grupozap-code-challenge.s3-website-us-east-1.amazonaws.com/sources/source-1.json"
}
```
- Retorna status 200, data de carregamento e lista de imóveis válidos ou inválidos salvos em mémória para os portais Zap e VivaReal.

#### 2 - Listagem de imóveis
- Dada a origem do portal em uma request o seu response será a listagem dos imóveis:
```
GET /realestates/{source}
```
- Exemplo:
```
GET /realestates/vivareal
```
- Foram implementados os seguintes filtros e paginação, pensando que a API pode ser consumida por vários tipos de clientes e com diferentes propósitos:
```
PagedParams (opcional): PageNumber (default 1), PageSize (default 10)
Filtros (opcional): City, BusinessType, Bathrooms, Bedrooms e ParkingSpaces
```
- Exemplo de parâmetros:
```
GET /realestates/zap?PageNumber=1&PageSize=10&City=São Paulo&Bathrooms=1&Bedrooms=2&ParkingSpaces=1
```
- Retorna status 200, lista de imóveis do portal correspondente, metadados de paginação e totais.
## Pontos de melhoria

## Instruções
Segue abaixo tutorial para rodar a aplicação localmente ou em conteiner Docker.

#### Pré-requisitos
- SDK do .NET 5.0.302
- Docker Desktop for Windows

#### Ambiente local
A aplicação foi desenvolvida no Visual Studio 2019. Porém, é possível interegir com a aplicação executando os comandos abaixo no diretório raiz do projeto:

- Executar aplicação:
```
dotnet run --project "./src/1 - Presentation/Challenge.RealEstates.API/Challenge.RealEstates.API.csproj"

https://localhost:5001/swagger/index.html
```
- Executar testes:
```
dotnet test
```
- Publicar projeto:
```
dotnet publish "./src/1 - Presentation/Challenge.RealEstates.API/Challenge.RealEstates.API.csproj" -c Release -o "<diretório>"
```
- Executar aplicação publicada:
```
dotnet Challenge.RealEstates.API.dll

https://localhost:5001/swagger/index.html
```

#### Container Docker
Para rodar a aplicação em Docker, execute os seguintes comandos no diretório raiz do projeto:

- Gerar imagem:
```
docker build . -t challenge-realestates-api
```
- Rodar a aplicação em container:
```
docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development challenge-realestates-api

http://localhost:8080/swagger/index.html
```

#### Production Read e Heroku

Para atender esse requisito, os commits do projeto passam por uma esteira no Git Actions com fases de  build e testes. A parte de deploy foi realizada manualmente com criação de uma imagem Docker. A aplicação foi publicada em um serviço de hospedagem gratuita chamada Heroku quer permite hospedar aplicações a partir de images de containeres (a aplicação demora cerca de 1 minuto para "acordar"). Segue link abaixo:

![pipeline](https://github.com/guisaulo/eng-zap-challenge-dotNet/actions/workflows/pipeline.yml/badge.svg)
```
https://challenge-realestates-api.herokuapp.com/swagger/index.html
```


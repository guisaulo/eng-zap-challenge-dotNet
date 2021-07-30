# Code Challenge Grupo ZAP

![pipeline](https://github.com/guisaulo/eng-zap-challenge-dotNet/actions/workflows/pipeline.yml/badge.svg)

Kanban: https://trello.com/b/W33srlBq/desafio-olx-eng-zap-challenge

## Decisões técnicas
## EndPoints
## Arquitetura
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

https://localhost:5001/swagger/index.html
```
- Rodar a aplicação em container:
```
docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development challenge-realestates-api

https://localhost:5001/swagger/index.html
```

#### Heroku

A aplicação foi publicada em um serviço de hospedagem gratuita chamada Heroku para fins de testes (a aplicação demora cerca de 1 minuto para "acordar"):
```
https://challenge-realestates-api.herokuapp.com/swagger/index.html
```


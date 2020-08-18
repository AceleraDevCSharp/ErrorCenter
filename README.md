# Projeto ErrorCenter
 
## Sumário
* [Desenvolvimento, por onde começar](#desenvolvimento-por-onde-começar)
* [Estrutura](#Estrutura)
* [Dependências](#dependências)
* [Bases de dados](#bases-de-dados)
* [Builds e testes](#builds-e-testes)
* [CI/CD](#ci/cd)
* [Deploy](#deploy)
 
## Desenvolvimento, por onde começar

## Estrutura

Padrão das camadas do projeto:

1. **ErrorCenter.Persistence.EF**: domínio da aplicação, responsável de manter as *regras de negócio* para a API;
2. **ErrorCenter.Services**: camada mais baixa, para acesso a dados, infraestrutura e serviços externos;
3. **ErrorCenter.WebAPI**: responsável pela camada de *disponibilização* dos endpoints da API;
4. **ErrorCenter.Tests**: responsável pela camada de *testes* dos projetos.

Formatação do projeto dentro do repositório:

```
├── ErrorCenter 
  ├── Business (Pasta Solução)
    ├── ErrorCenter.Services (projeto)
  ├── Data (Pasta Solução)
    ├── ErrorCenter.Domain (projeto)
  ├── Wiz.[NomeProjeto].API (projeto)
  ├── ErrorCenter.Tests (projeto)
├── ErrorCenter (solução)
```
 
## Dependências

* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2)
* [Patterns RESTful](http://standards.rest/)
 
## Bases de dados
 
## Build e Testes

* Obrigatoriedade de **não diminuir** os testes de cobertura.

### **Visual Studio**

1. Comandos para geração de build:
  + Debug: Executar via Test Explorer (adicionar breakpoint)
  + Release: Executar via Test Explorer (não adicionar breakpoint)

2. Ativar funcionalidade [dotCover](https://www.jetbrains.com/pt-br/dotcover/) para cobertura de testes.

A funcionalidade **dotCover** é paga e está disponível em qualquer versão do Visual Studio.

![Sem título](https://user-images.githubusercontent.com/40074126/90507542-6f47ab80-e12c-11ea-9d2c-1e646ad9033c.png)
 
## CI/CD
 
## Deploy 
 
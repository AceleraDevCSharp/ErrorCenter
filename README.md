# ErrorCenter

## Explicação da Arquitetura da Solução

* Projeto "ErrorCenter.WebApi"
    *  Nessa camada é o projeto principal, aqui será colocado os controladores, aqui irá receber as requisições e encaminhar para os serviços.

* Projeto "ErrorCenter.Domain"
    * Será criado as classes e atributos de negócio que seram mapeadas pelo Entity (Que é o framework de persistencia que estamos usando)

* Projeto "ErrorCenter.Persistence.EF"
    * Aqui se econtra as classes responsáveis pela configuração do entity framework e mapeamento do DB.
        * Como ja contém as migrations criadas, ao clonarem o repositório, abra o "Console de gerenciador de pacotes" do Visual Studio e de um "Update-Database -StartUpProject ErrorCenter.Persistence.EF" e o entity irá criar os DB,

* Projeto "ErrorCenter.Services"
    * Por sugestão do Abner, é nesse projeto que conteram as regras de negócio, validações e etc do projeto.

### Sobre a nomeclatura das pastas

* Como é dividido em camadas as separei dentro de "Pasta de Solução", que só é enxergada como pasta ao abrir a solução, se repararem no repositório no Github, elas não aparecem.

![Sem título.png](https://images.zenhubusercontent.com/5edbb45384926806b75b7ee2/2baa9e98-63dd-46ee-9fda-9638b6413f57)

### As referencias ficariam assim

![image.png](https://images.zenhubusercontent.com/5edbb45384926806b75b7ee2/c2ecac40-7074-4db1-b1f0-ce21e0b68b32)
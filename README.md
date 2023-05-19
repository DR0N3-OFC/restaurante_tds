# Restaurante Come Bem
Sistema de gerenciamento de pedidos de um restaurante, realizado como requisito de nota para a disciplina de Tecnologia em Desenvolvimento de Sistemas da UTFPR-MD.

## Instalação
Caso alguma exceção referente ao diretório `wwwroot` ocorra, possivelmente ele está faltando. Então você deverá criá-lo na raíz do projeto.

### Configurações do Docker
No diretório WebApplication1, ao fazer alterações no código, digite `docker-compose build` na linha de comando.

### Executando os Docker Containers
Para executar os contâiners, digite `docker-compose up -d` na linha de comando.

## Requisitos
* .NET 6.0;
* EntityFramework Core:
  * Para instalar digite `dotnet tool install --global dotnet-ef` e `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`;

## Rodando
* O site pode ser acessado por meio da url `http://localhost:7041`.
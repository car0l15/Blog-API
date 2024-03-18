# BlogAPI

Este projeto é uma API REST que serve como back-end para um fórum de estudantes. A API permite aos usuários criar novas postagens, editar postagens existentes, buscar postagens e muito mais.

## Tecnologias Utilizadas

- **SQL Server**: Gerenciamento do banco de dados.
- **ASP.NET Core 6**: Criação da aplicação web.
- **Entity Framework**: Conexão do banco de dados com a API.
- **XUnit** e **FluentAssertions**: Realização de testes.

## Como Executar a Aplicação

Como a aplicação utiliza um banco de dados local, será necessário executar um container Docker com uma imagem do SQL Server para realizar as requisições.

1. Execute `docker-compose up -d` para iniciar o container do Banco de Dados.
2. As informações necessárias para se conectar ao banco estão no arquivo `docker-compose.yaml`.
3. A senha para a conexão com o DB é Senha123$, o server é 127.0.0.1 e o username é SA
4. Após se conectar ao SQL Server, execute a query `Blog_Query.sql` na sua ferramenta de banco de dados.
5. Navegue até a pasta `src/Blog` e execute o comando `dotnet restore`, seguido de `dotnet run`.

## Rotas

!Rotas da API no Swagger

- As rotas `login` e `signup` são usadas para fazer login e cadastrar novos usuários. Quando a requisição é bem-sucedida, retorna um **token**.
- Para acessar as próximas rotas, é necessário clicar em Authorize e inserir `Bearer {token-gerado}`.
- As rotas `PUT post/id`, `POST post/id`, e `DELETE post/id` permitem alterar apenas posts feitos pelo usuário logado.
- As rotas `PUT user/id` e `DELETE user/id` permitem alterar apenas os dados do usuário logado.

## Implementações Futuras

- Maior cobertura de testes.
- Deploy da aplicação.
- Validação de Email.

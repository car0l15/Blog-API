# BlogAPI 

- Esse projeto consiste em uma API REST que retornaria informações para o front-end. A aplicação seria um fórum para estudantes, que permite os usuários
criar postagens novas, alterar postagens antigas, buscar postagens, etc. 


  <summary><strong>Tecnologias utilizadas:</strong></summary>
  Foi utilizado <strong>SQL Server</strong> para gerenciar o banco de dados e <strong>ASP.NET Core 6</strong> para criar a aplicação Web, 
  o ORM <strong>Entity Framework</strong> para realizar a conexão do banco de dados com a API, <strong>XUnit</strong> e <strong>FluentAssertions</strong>
  para realizar os testes.

  <br>
  <br>



  <summary><strong>Como rodar a aplicação:</strong></summary>
 Como a aplicação utiliza um banco de dados local será necessário rodar um container Docker com uma imagem SQL Server para conseguir realizar as requisições.
 <br>
  <ul>
    <li>Primeiro rode <code>docker-compose up -d</code> para subir o container com o Banco de Dados</li>
    <li>Você pode visualizar as informações que precisa para se conectar ao banco no arquivo docker-compose.yaml</li> 
    <li>Após conectar ao SQL Server, rode a query <code>Blog_Query.sql</code> na sua ferramenta de banco de dados</li>
    <li>Vá para a pasta src/Blog e rode o comando <code>dotnet restore</code>, seguido de <code>dotnet run</code></li>
  </ul>


<hr>

<details><summary><h2>Rotas</h2></summary>
  <img alt="Rotas da API no Swagger" src="./Images/Swagger.png"/>
  <ul>
   <li>As rotas <code>login</code> e <code>signup</code> fazem login e cadastro de usuários novos. Quando a requisição é bem sucedida retorna 
     um <strong>token</strong></li>
   </ul>
   <h3>Para acessar as próximas rotas é necessário inserir clicar em Authorize e inserir <code>Bearer {token-gerado}</code></h3>
  <ul>
   <li>As rotas <code>PUT post/id</code>, <code>POST post/id</code>, e <code>DELETE post/id</code> permitem alterar apenas posts feitos pelo usuário logado</li>
   <li>As rotas <code>PUT user/id</code> e <code>DELETE user/id</code> permitem alterar apenas alterar dados do usuário logado também</li>
  </ul>
</details>


<details><summary><h2>Implementações futuras</h2></summary>
   <ul>
     <li>Maior cobertura de testes</li>
     <li>Deploy da aplicação</li>
     <li>Validação de Email</li>
  </ul>
</details>

<hr>

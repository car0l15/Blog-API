using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using projeto_final.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using projeto_final.Models;
using System.Text;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Blog.Test;

public class TestUserController : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient client;   

    public TestUserController(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<BlogTestContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest2");
                });
                services.AddScoped<IBlogContext, BlogTestContext>();
                services.AddScoped<IBlogRepository, BlogRepository>();
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<BlogTestContext>())
                {
                    appContext.Database.EnsureCreated();
                    appContext.Database.EnsureDeleted();
                    appContext.Database.EnsureCreated();
                    appContext.Users.AddRange(
                        MockDB.GetUserListForTests()
                    );
                    appContext.Posts.AddRange(
                        MockDB.GetPostListForTests()
                    );
                    appContext.SaveChanges();
                }
            });
        }).CreateClient();
    }

    [Fact(DisplayName = "GET /user/{id} deve retornar status 200")]
    public async Task SuccesfulFindUser()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var token =  await httpResponse.Content.ReadAsStringAsync();
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/user/3fa85f64-5717-4562-b3fc-2c963f66afa6");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);        
        var httpResponse2 = await client.SendAsync(requestMessage);
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.OK);
        httpResponse2.Content.Should().NotBeNull();
    }

    [Fact(DisplayName = "GET /user/{id} deve retornar status 401 quando o token é inválido")]
    public async Task FailedFindUser_Unauthorized()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/user/3fa85f64-5717-4562-b3fc-2c963f66afa6");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "fakeToken");        
        var httpResponse2 = await client.SendAsync(requestMessage);
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "PUT /user/{id} deve retornar status 204")]
    public async Task SuccesfulUpdateUser()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var token =  await httpResponse.Content.ReadAsStringAsync();        
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var updatedUser = new User {Email = "teste@gmail.com", Password = "123456789", Username = "Novo username"}; 

        var httpResponse2 = await client.PutAsJsonAsync("/user/3fa85f64-5717-4562-b3fc-2c963f66afa6", updatedUser);
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact(DisplayName = "PUT /user/{id} deve retornar status 400 quando as informações enviadas são inválidas")]
    public async Task FailedUpdateUser_InvalidFormat()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var token =  await httpResponse.Content.ReadAsStringAsync();
        var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/user/3fa85f64-5717-4562-b3fc-2c963f66afa6");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);        
        requestMessage.Content = JsonContent.Create(new User {Email = "teste@gmail.com", Password = "123456789"});
        var httpResponse2 = await client.SendAsync(requestMessage);
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact(DisplayName = "PUT /user/{id} deve retornar status 401 quando o token é inválido")]
    public async Task FailedUpdateUser_Unauthorized()
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/user/3fa85f64-5717-4562-b3fc-2c963f66afa6");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "fakeToken");        
        var httpResponse2 = await client.SendAsync(requestMessage);
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

     [Fact(DisplayName = "DELETE /user/{id} deve retornar status 401 quando o token é inválido")]
    public async Task FailedDeleteUser_Unauthorized()
    {
        var httpResponse = await client.DeleteAsync("/user/3fa85f64-5717-4562-b3fc-2c963f66afa6");
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "DELETE /user/{id} deve retornar status 204")]
    public async Task SuccesfulDeleteUser()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var token =  await httpResponse.Content.ReadAsStringAsync();        
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var httpResponse2 = await client.DeleteAsync("/user/3fa85f64-5717-4562-b3fc-2c963f66afa6");
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

     [Fact(DisplayName = "DELETE /user/{id} deve retornar status 404")]
    public async Task FailedDeleteUser_NotFound()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var token =  await httpResponse.Content.ReadAsStringAsync();        
        var contentType = new MediaTypeWithQualityHeaderValue("application/json");
        client.DefaultRequestHeaders.Accept.Add(contentType);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var httpResponse2 = await client.DeleteAsync("/user/3fa85f64-5717-4562-b3fc-2c963f66af89");
        httpResponse2.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

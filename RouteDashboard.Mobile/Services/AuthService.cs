
using System.Net.Http.Json;

namespace RouteDashboard.Mobile.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5220/")
        };
    }

    public async Task<bool> CadastrarAsync(
        string nome,
        string email,
        string senha)
    {
        var dados = new
        {
            Name = nome,
            Email = email,
            Password = senha
        };

        var resposta = await _httpClient.PostAsJsonAsync(
            "api/Auth/register",
            dados
        );

        return resposta.IsSuccessStatusCode;
    }
}

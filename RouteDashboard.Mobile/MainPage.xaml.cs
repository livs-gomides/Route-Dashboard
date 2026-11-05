
using System.Net.Http.Json;

namespace RouteDashboard.Mobile;

public partial class MainPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public MainPage()
    {
        InitializeComponent();

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5220/")
        };
    }

    private async void OnCadastroClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CadastroPage));
    }

    private async void OnEntrarClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string senha = PasswordEntry.Text ?? "";

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha))
        {
            await DisplayAlert("Atenção", "Informe seu e-mail e senha.", "OK");
            return;
        }

        try
        {
            var dados = new
            {
                Email = email,
                Password = senha
            };

            var resposta = await _httpClient.PostAsJsonAsync(
                "api/Auth/login",
                dados
            );

            if (resposta.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Login realizado!", "OK");

                // Próxima etapa: abrir o Dashboard.
            }
            else
            {
                await DisplayAlert(
                    "Login",
                    "E-mail ou senha incorretos.",
                    "OK"
                );
            }
        }
        catch (Exception)
        {
            await DisplayAlert(
                "Erro",
                "Não foi possível conectar à API.",
                "OK"
            );
        }
    }
}

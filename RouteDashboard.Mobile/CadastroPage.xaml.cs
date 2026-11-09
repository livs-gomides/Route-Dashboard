
using RouteDashboard.Mobile.Services;

namespace RouteDashboard.Mobile;

public partial class CadastroPage : ContentPage
{
    private readonly AuthService _authService;

    public CadastroPage()
    {
        InitializeComponent();
        _authService = new AuthService();
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        string nome = NomeEntry.Text?.Trim() ?? "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string senha = SenhaEntry.Text ?? "";
        string confirmarSenha = ConfirmarSenhaEntry.Text ?? "";

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(senha))
        {
            await DisplayAlert("Atenção", "Preencha todos os campos.", "OK");
            return;
        }

        if (senha.Length < 8)
        {
            await DisplayAlert("Atenção", "A senha deve ter pelo menos 8 caracteres.", "OK");
            return;
        }

        if (senha != confirmarSenha)
        {
            await DisplayAlert("Atenção", "As senhas não coincidem.", "OK");
            return;
        }

        try
        {
            bool sucesso = await _authService.CadastrarAsync(nome, email, senha);

            if (sucesso)
            {
                await DisplayAlert(
                    "Sucesso",
                    "Conta criada! Você já pode fazer login.",
                    "OK"
                );

                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert(
                    "Cadastro",
                    "Não foi possível criar a conta. Verifique se o e-mail já está cadastrado.",
                    "OK"
                );
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao criar a conta: {ex.Message}", "OK");
        }
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}

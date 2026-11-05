
namespace RouteDashboard.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            nameof(CadastroPage),
            typeof(CadastroPage)
        );
    }
}

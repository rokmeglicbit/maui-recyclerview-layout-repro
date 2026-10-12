namespace Repro;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("edit", typeof(EditPage));
    }
}

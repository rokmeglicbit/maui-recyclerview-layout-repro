namespace Repro;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    async void OnEditClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("edit", animate: false);

    // Posnemanje: "Save -> list -> hitro spet Edit", 3x zapored
    async void OnEditFastClicked(object sender, EventArgs e)
    {
        for (int i = 0; i < 3; i++)
        {
            await Shell.Current.GoToAsync("edit", animate: false);
            await Shell.Current.GoToAsync("..", animate: false);
        }
        await Shell.Current.GoToAsync("edit", animate: false);
    }
}

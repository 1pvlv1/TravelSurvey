
namespace TravelSurvey;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnRequiredFieldsChanged(
        object sender, TextChangedEventArgs e)
    {
        ShowResultButton.IsEnabled =
            !string.IsNullOrWhiteSpace(NameEntry.Text) &&
            !string.IsNullOrWhiteSpace(EmailEntry.Text);
    }

    private async void OnShowResultClicked(
        object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert(
                "Ошибка",
                "Заполни имя и электронную почту!",
                "OK");
            return;
        }

        if (!EmailEntry.Text.Contains('@'))
        {
            await DisplayAlert(
                "Ошибка",
                "Проверь правильность электронной почты!",
                "OK");
            return;
        }

        string tripType = "Не выбран";

        if (BeachRadio.IsChecked)
            tripType = "Пляжный отдых";
        else if (AdventureRadio.IsChecked)
            tripType = "Приключения";
        else if (ExcursionRadio.IsChecked)
            tripType = "Экскурсии";

        string country =
            CountryPicker.SelectedItem?.ToString() ?? "Не выбрана";

        string result =
            $"Имя: {NameEntry.Text}\n" +
            $"Почта: {EmailEntry.Text}\n" +
            $"Страна: {country}\n" +
            $"Тип отдыха: {tripType}\n" +
            $"Авиабилет: {(FlightCheckBox.IsChecked ? "Да" : "Нет")}\n" +
            $"Проживание: {(HotelCheckBox.IsChecked ? "Да" : "Нет")}\n" +
            $"Трансфер: {(TransferSwitch.IsToggled ? "Да" : "Нет")}\n" +
            $"Дата: {TravelDatePicker.Date:dd.MM.yyyy}\n" +
            $"Бюджет: {BudgetSlider.Value:F0}\n" +
            $"Пожелания: {WishesEditor.Text}";

        ResultLabel.Text = result;
        ResultBorder.IsVisible = true;
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        NameEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;

        CountryPicker.SelectedIndex = -1;

        BeachRadio.IsChecked = false;
        AdventureRadio.IsChecked = false;
        ExcursionRadio.IsChecked = false;

        FlightCheckBox.IsChecked = false;
        HotelCheckBox.IsChecked = false;

        TransferSwitch.IsToggled = false;

        TravelDatePicker.Date =
            TravelDatePicker.MinimumDate;

        BudgetSlider.Value = 125000;

        WishesEditor.Text = string.Empty;

        ResultLabel.Text = string.Empty;
        ResultBorder.IsVisible = false;

        ShowResultButton.IsEnabled = false;
    }
}

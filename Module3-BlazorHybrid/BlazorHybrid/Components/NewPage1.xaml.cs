using BlazorHybrid.Services;

namespace BlazorHybrid.Components;

public partial class NewPage1 : ContentPage
{
    private readonly CounterService _counterService;
	public NewPage1(CounterService counterService)
	{
		InitializeComponent();
		_counterService = counterService;
        CountLabel.Text = _counterService.Count.ToString();
        _counterService.CountingUp += (currentcount) => { CountLabel.Text = currentcount.ToString(); };
    }
    private async void OnCountUpClicked(object sender, EventArgs e)
    {
        await _counterService.CountUpAsync();

    }
}
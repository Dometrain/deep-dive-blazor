using BlazorHybrid.Components;
using BlazorHybrid.Components.Pages;

namespace BlazorHybrid
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));
        }
    }
}

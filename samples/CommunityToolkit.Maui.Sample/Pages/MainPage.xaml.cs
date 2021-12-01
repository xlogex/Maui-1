using System;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Sample.Pages;

public partial class MainPage : BasePage
{
	public MainPage()
	{
		InitializeComponent();

		Page ??= this;

		Padding = new Thickness(20, 0);
	}

	void Bla(object sender, EventArgs args)
	{
		var p = new CommunityToolkit.Maui.UI.Views.Popup() 
		{
			IsLightDismissEnabled = true,
		};

		_ = p.Result;

		var layout = new Label { Text = "I'm a popup!" };
		p.Content = new VerticalStackLayout
		{
			Children = { layout },
			HeightRequest = 300,
			WidthRequest = 300
		};
		Navigation.ShowPopup(p);

		Device.StartTimer(TimeSpan.FromSeconds(5), () =>
		{
			p.Color = Colors.Red;
			return false;
		});
	}
}
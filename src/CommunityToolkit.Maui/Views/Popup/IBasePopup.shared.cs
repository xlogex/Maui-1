using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.UI.Views;

public interface IBasePopup : IElement
{
	IView? Anchor { get; }
	Color Color { get; }
	IView? Content { get; }
	LayoutOptions HorizontalOptions { get; }
	bool IsLightDismissEnabled { get; }
	Size Size { get; }
	LayoutOptions VerticalOptions { get; }

	void OnDismissed(object? result);
	void OnOpened();
}
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using LayoutAlignment = Microsoft.Maui.Primitives.LayoutAlignment;
namespace CommunityToolkit.Maui.UI.Views;

public interface IBasePopup : IElement
{
	IView? Anchor { get; }
	Color Color { get; }
	IView? Content { get; }
	LayoutAlignment HorizontalOptions { get; }
	bool IsLightDismissEnabled { get; }
	Size Size { get; }
	LayoutAlignment VerticalOptions { get; }

	void OnDismissed(object? result);
	void OnOpened();

	void LightDismiss();
}
using System;
using Microsoft.Maui;
using AView = Android.Views.View;
using Microsoft.Maui.Handlers;

namespace CommunityToolkit.Maui.UI.Views;

public partial class PopupViewHandler : ElementHandler<IBasePopup, PopupRenderer>
{
	internal AView? Container { get; set; }

	public static void MapOnDismissed(PopupViewHandler handler, IBasePopup view, object? result)
	{
		handler.NativeView?.Dismiss();
	}

	public static void MapOnOpened(PopupViewHandler handler, IBasePopup view, object? result)
	{
		handler.NativeView?.Show();
	}

	public static void MapAnchor(PopupViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetAnchor(view);
	}

	public static void MapLightDismiss(PopupViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetLightDismiss(view);
	}

	public static void MapColor(PopupViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetColor(view);
	}

	public static void MapSize(PopupViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetSize(view, handler.Container);
		handler.NativeView?.SetAnchor(view);
	}

	protected override PopupRenderer CreateNativeElement()
	{
		return new PopupRenderer(MauiContext!.Context!, MauiContext);
	}

	protected override void ConnectHandler(PopupRenderer nativeView)
	{
		Container = nativeView.SetElement(VirtualView);
		nativeView.ShowEvent += OnShowed;
	}

	void OnShowed(object? sender, EventArgs args)
	{
		VirtualView?.OnOpened();
	}

	protected override void DisconnectHandler(PopupRenderer nativeView)
	{
		nativeView.ShowEvent -= OnShowed;
		nativeView.Dispose();
	}
}

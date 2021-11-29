using System;
using Microsoft.Maui;
using AView = Android.Views.View;
using Microsoft.Maui.Handlers;
using CommunityToolkit.Maui.Platform;

namespace CommunityToolkit.Maui.UI.Views;

public partial class PopupViewHandler : ElementHandler<IBasePopup, MCTPopup>
{
	public static PropertyMapper<IBasePopup, PopupViewHandler> PopUpMapper = new(ElementMapper)
	{
		[nameof(IBasePopup.Anchor)] = MapAnchor,
		[nameof(IBasePopup.Color)] = MapColor,
		[nameof(IBasePopup.Size)] = MapSize,
		[nameof(IBasePopup.VerticalOptions)] = MapSize,
		[nameof(IBasePopup.HorizontalOptions)] = MapSize,
		[nameof(IBasePopup.IsLightDismissEnabled)] = MapLightDismiss
	};

	public static CommandMapper<IBasePopup, PopupViewHandler> PopUpCommandMapper = new(ElementCommandMapper)
	{
		[nameof(IBasePopup.OnDismissed)] = MapOnDismissed,
		[nameof(IBasePopup.OnOpened)] = MapOnOpened
	};

	public PopupViewHandler(PropertyMapper? mapper, CommandMapper? commandMapper)
		: base(mapper ?? PopUpMapper, commandMapper ?? PopUpCommandMapper)
	{
	}


	public PopupViewHandler()
		: base(PopUpMapper, PopUpCommandMapper)
	{
	}


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

	protected override MCTPopup CreateNativeElement()
	{
		return new MCTPopup(MauiContext!.Context!, MauiContext);
	}

	protected override void ConnectHandler(MCTPopup nativeView)
	{
		Container = nativeView.SetElement(VirtualView);
		nativeView.ShowEvent += OnShowed;
	}

	void OnShowed(object? sender, EventArgs args)
	{
		VirtualView?.OnOpened();
	}

	private protected override void OnDisconnectHandler(object nativeView)
	{
		base.OnDisconnectHandler(nativeView);
	}

	protected override void DisconnectHandler(MCTPopup nativeView)
	{
		nativeView.ShowEvent -= OnShowed;
		nativeView.Dispose();
	}
}

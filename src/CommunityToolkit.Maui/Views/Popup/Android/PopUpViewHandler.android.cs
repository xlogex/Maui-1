using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;

namespace CommunityToolkit.Maui.UI.Views;

public partial class PopUpViewHandler : ElementHandler<IBasePopup, PopupRenderer>
{
	public static PropertyMapper<IBasePopup, PopUpViewHandler> PopUpMapper = new(ElementMapper)
	{
		[nameof(IBasePopup.Anchor)] = MapAnchor,
		[nameof(IBasePopup.Color)] = MapColor,
		[nameof(IBasePopup.Size)] = MapSize,
	};

	public static CommandMapper<IBasePopup, PopUpViewHandler> PopUpCommandMapper = new(ElementCommandMapper) 
	{
		[nameof(IBasePopup.OnDismissed)] = MapOnDismissed,
	};

	public static void MapOnDismissed(PopUpViewHandler handler, IBasePopup view, object? result)
	{
		handler.NativeView?.Dismiss();
	}

	public static void MapAnchor(PopUpViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetAnchor(view);
	}

	public static void MapColor(PopUpViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetColor(view);
	}

	public static void MapSize(PopUpViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetColor(view);
	}

	public PopUpViewHandler(PropertyMapper? mapper, CommandMapper? commandMapper)
		: base (mapper ?? PopUpMapper, commandMapper ?? PopUpCommandMapper)
	{
	}


	public PopUpViewHandler()
		: base (PopUpMapper, PopUpCommandMapper)
	{
	}

	protected override PopupRenderer CreateNativeElement()
	{
		return new PopupRenderer(MauiContext!.Context!, MauiContext);
	}

	protected override void ConnectHandler(PopupRenderer nativeView)
	{
		nativeView.SetElement(VirtualView);
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

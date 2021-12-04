using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Platform;
using Microsoft.Maui.Handlers;

namespace CommunityToolkit.Maui.UI.Views;
public partial class PopupViewHandler : ElementHandler<IBasePopup, PopupRenderer>
{
	public static void MapOnDismissed(PopupViewHandler handler, IBasePopup view, object? result)
	{
	}

	public static void MapOnOpened(PopupViewHandler handler, IBasePopup view, object? result)
	{
	}

	public static void MapOnLightDismiss(PopupViewHandler handler, IBasePopup view, object? result)
	{
	}

	public static void MapAnchor(PopupViewHandler handler, IBasePopup view)
	{

	}

	public static void MapLightDismiss(PopupViewHandler handler, IBasePopup view)
	{
	}

	public static void MapColor(PopupViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetBackgroundColor(view);
	}

	public static void MapSize(PopupViewHandler handler, IBasePopup view)
	{
		handler.NativeView?.SetSize(view);
		handler.NativeView?.SetLayout(view);
	}

	protected override void ConnectHandler(PopupRenderer nativeView)
	{
		base.ConnectHandler(nativeView);
		nativeView.SetElement(VirtualView);
	}

	protected override PopupRenderer CreateNativeElement()
	{
		return new PopupRenderer(MauiContext ?? throw new NullReferenceException(nameof(MauiContext)));
	}
}

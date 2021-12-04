using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

namespace CommunityToolkit.Maui.UI.Views;
public partial class PopupViewHandler : ElementHandler<IBasePopup, object>
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
	}

	public static void MapSize(PopupViewHandler handler, IBasePopup view)
	{
	}

	protected override object CreateNativeElement()
	{
		throw new NotImplementedException();
	}
}

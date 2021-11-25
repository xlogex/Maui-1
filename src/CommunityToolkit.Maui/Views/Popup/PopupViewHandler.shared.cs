using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.UI.Views;

public partial class PopupViewHandler
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
}

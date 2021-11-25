using System.Threading.Tasks;
using CommunityToolkit.Maui.UI.Views;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Extensions;

public static partial class NavigationExtensions
{
	static IElementHandler PlatformShowPopup(BasePopup popup, IMauiContext mauiContext)
	{
		var x = popup.ToHandler(mauiContext);
		x.Invoke(nameof(IBasePopup.OnOpened));
		return x;
	}

	static Task<T?> PlatformShowPopupAsync<T>(Popup<T> popup, IMauiContext mauiContext)
	{
		PlatformShowPopup(popup, mauiContext);
		
		return popup.Result;
	}
}

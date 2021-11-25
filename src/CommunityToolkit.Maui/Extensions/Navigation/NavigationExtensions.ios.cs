using System.Threading.Tasks;
using CommunityToolkit.Maui.UI.Views;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Extensions;

public static partial class NavigationExtensions
{
	static void PlatformShowPopup(BasePopup popup, IMauiContext mauiContext)
	{
		_ = popup.ToNative(mauiContext);
	}

	static Task<T?> PlatformShowPopupAsync<T>(Popup<T> popup, IMauiContext mauiContext)
	{
		PlatformShowPopup(popup, mauiContext);
		return popup.Result;
	}
}

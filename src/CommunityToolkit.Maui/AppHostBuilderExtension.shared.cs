using CommunityToolkit.Maui.UI.Views;
using Microsoft.Maui.Hosting;

namespace CommunityToolkit.Maui.Hosting;
public static class AppHostBuilderExtension
{
	public static MauiAppBuilder UseCommunityToolkit(this MauiAppBuilder builder)
	{
		builder.ConfigureMauiHandlers(h =>
		{
#if __ANDROID__ || __IOS__
		   h.AddHandler(typeof(BasePopup), typeof(PopupViewHandler));
		   h.AddHandler(typeof(Popup), typeof(PopupViewHandler));
#endif
		});

		return builder;
	}
}

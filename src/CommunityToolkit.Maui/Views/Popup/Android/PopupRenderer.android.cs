using System;
using Android.App;
using Android.Content;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Platform;
using AView = Android.Views.View;

namespace CommunityToolkit.Maui.UI.Views;

public class PopupRenderer : Dialog, IDialogInterfaceOnCancelListener
{
	AView? container;
	bool isDisposed;

	readonly IMauiContext mauiContext;

	public IBasePopup? VirtualView { get; private set; }

	public PopupRenderer(Context context, IMauiContext mauiContext)
		: base(context)
	{
		this.mauiContext = mauiContext ?? throw new ArgumentNullException(nameof(mauiContext));
	}
	
	public AView? SetElement(IBasePopup? element)
	{
		if (element == null)
			throw new ArgumentNullException(nameof(element));
		
		VirtualView = element;
		CreateControl(VirtualView);

		return container;
	}

	protected virtual void OnElementChanged(ElementChangedEventArgs<BasePopup?> e)
	{
		if (e.NewElement != null && !isDisposed && VirtualView is BasePopup basePopup)
		{
			SetEvents(basePopup);
			//this.SetColor(basePopup);
			//this.SetSize(basePopup, container);
			//this.SetAnchor(basePopup);
			//this.SetLightDismiss(basePopup);

			//Show();
		}
	}

	public override void Show()
	{
		base.Show();
		VirtualView?.OnOpened();
	}

	public void CreateControl(in IBasePopup basePopup)
	{
		if (basePopup.Content != null)
		{
			container = basePopup.Content.ToNative(mauiContext);
			SetContentView(container);
		}
	}

	void SetEvents(in BasePopup basePopup)
	{
		SetOnCancelListener(this);
		basePopup.Dismissed += OnDismissed;
	}

	void OnDismissed(object? sender, PopupDismissedEventArgs e)
	{
		if (IsShowing)
			Dismiss();
	}

	public void OnCancel(IDialogInterface? dialog)
	{
		//if (Element?.IsLightDismissEnabled is true)
		//	Element.LightDismiss();
	}

	//protected override void Dispose(bool disposing)
	//{
	//	if (isDisposed)
	//		return;

	//	isDisposed = true;
	//	if (disposing)
	//	{

	//		if (Element != null)
	//		{
	//			Element.PropertyChanged -= OnElementPropertyChanged;
	//			Element = null;
	//		}
	//	}

	//	base.Dispose(disposing);
	//}
}

using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using static Android.App.ActionBar;
using AColorRes = Android.Resource.Color;
using AView = Android.Views.View;
using GravityFlags = Android.Views.GravityFlags;

namespace CommunityToolkit.Maui.UI.Views;

public class PopupRenderer : Dialog, IDialogInterfaceOnCancelListener
{
	int? defaultLabelFor;
	AView? container;
	bool isDisposed;

	public IBasePopup? Element { get; private set; }

	public PopupRenderer(Context context, IMauiContext mauiContext)
		: base(context)
	{
		this.mauiContext = mauiContext;
	}

	IMauiContext mauiContext;
	
	public AView? SetElement(IBasePopup? element)
	{
		if (element == null)
			throw new ArgumentNullException(nameof(element));

		//if (element is not BasePopup popup)
		//	throw new ArgumentNullException("Element is not of type " + typeof(BasePopup), nameof(element));

		var oldElement = Element;
		
		Element = element;
		CreateControl(Element);

		return container;

		//if (oldElement != null)
		//	oldElement.PropertyChanged -= OnElementPropertyChanged;

		//element.PropertyChanged += OnElementPropertyChanged;

		//OnElementChanged(new ElementChangedEventArgs<BasePopup?>(oldElement, Element));
	}

	protected virtual void OnElementChanged(ElementChangedEventArgs<BasePopup?> e)
	{
		if (e.NewElement != null && !isDisposed && Element is BasePopup basePopup)
		{
			SetEvents(basePopup);
			this.SetColor(basePopup);
			this.SetSize(basePopup, container);
			this.SetAnchor(basePopup);
			this.SetLightDismiss(basePopup);

			Show();
		}
	}

	public override void Show()
	{
		base.Show();
		Element?.OnOpened();
	}

	public void CreateControl(in IBasePopup basePopup)
	{
		if (basePopup.Content != null && mauiContext != null)
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

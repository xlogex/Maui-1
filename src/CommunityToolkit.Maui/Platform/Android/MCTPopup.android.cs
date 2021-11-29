using System;
using Android.App;
using Android.Content;
using CommunityToolkit.Maui.UI.Views;
using Microsoft.Maui;
using AView = Android.Views.View;

namespace CommunityToolkit.Maui.Platform;

public class MCTPopup : Dialog, IDialogInterfaceOnCancelListener
{
	AView? container;
	bool isDisposed;

	readonly IMauiContext mauiContext;

	public IBasePopup? VirtualView { get; private set; }

	public MCTPopup(Context context, IMauiContext mauiContext)
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
		SetEvents(element);
		return container;
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

	void SetEvents(in IBasePopup basePopup)
	{
		SetOnCancelListener(this);
		if (basePopup is BasePopup bp)
			bp.Dismissed += OnDismissed;
	}

	void OnDismissed(object? sender, PopupDismissedEventArgs e)
	{
		if (IsShowing)
			Dismiss();
	}

	public void OnCancel(IDialogInterface? dialog)
	{
		if (VirtualView?.IsLightDismissEnabled is true)
			VirtualView.LightDismiss();
	}

	protected override void Dispose(bool disposing)
	{
		if (isDisposed)
			return;

		isDisposed = true;
		if (disposing)
		{

			if (VirtualView is BasePopup bp)
			{
				bp.Dismissed -= OnDismissed;
				VirtualView = null;
			}
		}

		base.Dispose(disposing);
	}
}

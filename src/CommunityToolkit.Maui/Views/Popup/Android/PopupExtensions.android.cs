using System;
using Android.App;
using Microsoft.Maui;
using Android.Views;
using Microsoft.Maui.Controls;
using Android.Graphics.Drawables;
using AColorRes = Android.Resource.Color;

namespace CommunityToolkit.Maui.UI.Views;
public static class PopupExtensions
{
	public static void SetAnchor(this Dialog dialog, IBasePopup basePopup)
	{
		var window = GetWindow(dialog);

		if (basePopup.Handler is null || basePopup.Handler.MauiContext is null)
			return;

		var mauiContext = basePopup.Handler.MauiContext;
		if (basePopup.Anchor != null)
		{
			var anchorView = basePopup.Anchor?.ToNative(mauiContext);

			if (anchorView is null)
				return;

			var locationOnScreen = new int[2];
			anchorView.GetLocationOnScreen(locationOnScreen);

			window?.SetGravity(GravityFlags.Top | GravityFlags.Left);
			window?.DecorView.Measure((int)MeasureSpecMode.Unspecified, (int)MeasureSpecMode.Unspecified);

			// This logic is tricky, please read these notes if you need to modify
			// Android window coordinate starts (0,0) at the top left and (max,max) at the bottom right. All of the positions
			// that are being handled in this operation assume the point is at the top left of the rectangle. This means the
			// calculation operates in this order:
			// 1. Calculate top-left position of Anchor
			// 2. Calculate the Actual Center of the Anchor by adding the width /2 and height / 2
			// 3. Calculate the top-left point of where the dialog should be positioned by subtracting the Width / 2 and height / 2
			//    of the dialog that is about to be drawn.
			_ = window?.Attributes ?? throw new NullReferenceException();

			window.Attributes.X = locationOnScreen[0] + (anchorView.Width / 2) - (window.DecorView.MeasuredWidth / 2);
			window.Attributes.Y = locationOnScreen[1] + (anchorView.Height / 2) - (window.DecorView.MeasuredHeight / 2);
		}
		else
		{
			SetDialogPosition(basePopup, window);
		}
	}

	static Android.Views.Window GetWindow(Dialog dialog)
	{
		var window = dialog.Window ?? throw new Exception("WIndows is null!");

		return window;
	}

	public static void SetColor(this Dialog dialog, IBasePopup basePopup)
	{
		var window = GetWindow(dialog);
		window?.SetBackgroundDrawable(new ColorDrawable(basePopup.Color.ToNative(AColorRes.BackgroundLight, dialog.Context)));
	}

	public static void SetSize(this Dialog dialog, IBasePopup basePopup)
	{
		if (basePopup.Content != null && basePopup.Size != default)
		{
			var decorView = (ViewGroup)(Window?.DecorView ?? throw new NullReferenceException());
			var child = decorView?.GetChildAt(0) ?? throw new NullReferenceException();

			var realWidth = (int)Context.ToPixels(basePopup.Size.Width);
			var realHeight = (int)Context.ToPixels(basePopup.Size.Height);

			var realContentWidth = (int)Context.ToPixels(basePopup.Content.WidthRequest);
			var realContentHeight = (int)Context.ToPixels(basePopup.Content.HeightRequest);

			var childLayoutParams = (FrameLayout.LayoutParams)(child?.LayoutParameters ?? throw new NullReferenceException());
			childLayoutParams.Width = realWidth;
			childLayoutParams.Height = realHeight;
			child.LayoutParameters = childLayoutParams;

			var horizontalParams = -1;
			switch (basePopup.Content.HorizontalOptions.Alignment)
			{
				case LayoutAlignment.Center:
				case LayoutAlignment.End:
				case LayoutAlignment.Start:
					horizontalParams = LayoutParams.WrapContent;
					break;
				case LayoutAlignment.Fill:
					horizontalParams = LayoutParams.MatchParent;
					break;
			}

			var verticalParams = -1;
			switch (basePopup.Content.VerticalOptions.Alignment)
			{
				case LayoutAlignment.Center:
				case LayoutAlignment.End:
				case LayoutAlignment.Start:
					verticalParams = LayoutParams.WrapContent;
					break;
				case LayoutAlignment.Fill:
					verticalParams = LayoutParams.MatchParent;
					break;
			}

			_ = container ?? throw new NullReferenceException();
			if (realContentWidth > -1)
			{
				var inputMeasuredWidth = realContentWidth > realWidth ?
					realWidth : realContentWidth;
				container.Measure(inputMeasuredWidth, (int)MeasureSpecMode.Unspecified);
				horizontalParams = container.MeasuredWidth;
			}
			else
			{
				container.Measure(realWidth, (int)MeasureSpecMode.Unspecified);
				horizontalParams = container.MeasuredWidth > realWidth ?
					realWidth : container.MeasuredWidth;
			}

			if (realContentHeight > -1)
			{
				verticalParams = realContentHeight;
			}
			else
			{
				var inputMeasuredWidth = realContentWidth > -1 ? horizontalParams : realWidth;
				container.Measure(inputMeasuredWidth, (int)MeasureSpecMode.Unspecified);
				verticalParams = container.MeasuredHeight > realHeight ?
					realHeight : container.MeasuredHeight;
			}

			var containerLayoutParams = new FrameLayout.LayoutParams(horizontalParams, verticalParams);

			switch (basePopup.Content.VerticalOptions.Alignment)
			{
				case LayoutAlignment.Start:
					containerLayoutParams.Gravity = GravityFlags.Top;
					break;
				case LayoutAlignment.Center:
				case LayoutAlignment.Fill:
					containerLayoutParams.Gravity = GravityFlags.FillVertical;
					containerLayoutParams.Height = realHeight;
					//container.MatchHeight = true;
					break;
				case LayoutAlignment.End:
					containerLayoutParams.Gravity = GravityFlags.Bottom;
					break;
			}

			switch (basePopup.Content.HorizontalOptions.Alignment)
			{
				case LayoutAlignment.Start:
					containerLayoutParams.Gravity |= GravityFlags.Left;
					break;
				case LayoutAlignment.Center:
				case LayoutAlignment.Fill:
					containerLayoutParams.Gravity |= GravityFlags.FillHorizontal;
					containerLayoutParams.Width = realWidth;
					//container.MatchWidth = true;
					break;
				case LayoutAlignment.End:
					containerLayoutParams.Gravity |= GravityFlags.Right;
					break;
			}

			container.LayoutParameters = containerLayoutParams;
		}
	}

	static void SetDialogPosition(in IBasePopup basePopup, Android.Views.Window window)
	{
		var gravityFlags = basePopup.VerticalOptions.Alignment switch
		{
			LayoutAlignment.Start => GravityFlags.Top,
			LayoutAlignment.End => GravityFlags.Bottom,
			_ => GravityFlags.CenterVertical,
		};
		gravityFlags |= basePopup.HorizontalOptions.Alignment switch
		{
			LayoutAlignment.Start => GravityFlags.Left,
			LayoutAlignment.End => GravityFlags.Right,
			_ => GravityFlags.CenterHorizontal,
		};
		window?.SetGravity(gravityFlags);
	}
}

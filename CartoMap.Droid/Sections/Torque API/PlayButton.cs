﻿using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace CartoMap.Droid
{
	public class PlayButton : RelativeLayout
	{
		public static Color Carto = Color.Rgb(215, 82, 75);

		public new EventHandler<EventArgs> Click;

		const int RESOURCE_PLAY = Resource.Drawable.button_play;
		const int RESOURCE_PAUSE = Resource.Drawable.button_pause;

		ImageView imageView;

		int imageResource;
		public int ImageResource
		{
			get { return imageResource; }
			set
			{
				imageResource = value;
				imageView.SetImageResource(imageResource);
			}
		}

		public override ViewGroup.LayoutParams LayoutParameters
		{
			get
			{
				return base.LayoutParameters;
			}
			set
			{
				base.LayoutParameters = value;

				GradientDrawable drawable = new GradientDrawable();
				drawable.SetCornerRadius(value.Width / 2);
				drawable.SetColor(Carto);
				Background = drawable;
			}
		}

		public bool IsPaused { 
			get { return imageResource == RESOURCE_PLAY; } 
		}

		public PlayButton(Context context) : base(context)
		{
			imageView = new ImageView(context);
			AddView(imageView);

			var parameters = new RelativeLayout.LayoutParams(
				RelativeLayout.LayoutParams.MatchParent, 
				RelativeLayout.LayoutParams.MatchParent
			);
			parameters.SetMargins(10, 10, 10, 10);
			imageView.LayoutParameters = parameters;

			int buttonSize = 105;
			int buttonPadding = 30;

			parameters = new RelativeLayout.LayoutParams(buttonSize, buttonSize);
			parameters.AddRule(LayoutRules.AlignParentRight);
			parameters.AddRule(LayoutRules.AlignParentBottom);
			parameters.SetMargins(0, 0, buttonPadding, buttonPadding);

			LayoutParameters = parameters;

			ImageResource = RESOURCE_PAUSE;

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				Elevation = 10;
			}

			SetPadding(10, 10, 10, 10);
		}

		public void Toggle()
		{
			if (IsPaused)
			{
				ImageResource = RESOURCE_PAUSE;
			}
			else {
				ImageResource = RESOURCE_PLAY;
			}
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			if (e.Action == MotionEventActions.Down)
			{
				AnimateToScale(1.02f);
			}
			else if (e.Action == MotionEventActions.Up)
			{
				AnimateToScale(1.0f);

				Toggle();

				if (Click != null)
				{
					Click(this, EventArgs.Empty);
				}
			}
			else if (e.Action == MotionEventActions.Cancel)
			{
				AnimateToScale(1.0f);
			}

			return true;
		}

		ViewPropertyAnimator AnimateToScale(float scale)
		{
			return Animate().ScaleY(scale).ScaleX(scale).SetDuration(100);
		}
	}
}

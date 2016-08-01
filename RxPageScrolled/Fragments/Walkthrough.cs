using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace RxPageScrolled
{
	public class Walkthrough : Android.Support.V4.App.Fragment
	{
		public Color BackgroundColor { get; set; }

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = Activity.LayoutInflater.Inflate(Resource.Layout.Walkthrough, null);
			base.OnCreateView (inflater, container, savedInstanceState);
			return view;
		}
	}
}


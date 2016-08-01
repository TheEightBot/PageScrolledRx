using System;
using Android.App;
using System.Collections.Generic;

namespace RxPageScrolled
{
	internal class WalkthroughAdapter : Android.Support.V4.App.FragmentStatePagerAdapter
	{
		readonly IList<Walkthrough> _walkthroughs;

		public WalkthroughAdapter(Android.Support.V4.App.FragmentManager fm, IList<Walkthrough> walkthroughs) : base(fm)
		{
			_walkthroughs = walkthroughs;
		}

		public override Android.Support.V4.App.Fragment GetItem(int position)
		{
			return position <= _walkthroughs.Count-1 
				? _walkthroughs[position]
				: null;
		}

		public override int Count
		{
			get { return _walkthroughs.Count; }
		}

	}
}


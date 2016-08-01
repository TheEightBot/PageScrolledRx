using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Graphics;
using Android.Support.V4.App;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System;

namespace RxPageScrolled
{
	[Activity (Label = "RxPageScrolled", MainLauncher = true)]
	public class WalkthroughContainer : FragmentActivity
	{
		ViewPager _viewPager;
		WalkthroughAdapter _pagerAdapter;

		public Lazy<CompositeDisposable> ControlBindings { get; private set; } = new Lazy<CompositeDisposable>();

		enum ScrollDirection { 
			Left,
			Center,
			Right
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			this.ActionBar.Hide();
			SetContentView (Resource.Layout.WalkthroughContainer);

			_pagerAdapter = new WalkthroughAdapter(
				this.SupportFragmentManager, 
				new [] {
					new Walkthrough { BackgroundColor = Color.DarkSlateBlue },
					new Walkthrough { BackgroundColor = Color.DarkOrange },
					new Walkthrough { BackgroundColor = Color.ForestGreen }
				});
			
			_viewPager = this.FindViewById<ViewPager>(Resource.Id.walkthroughContainer_pager);
			_viewPager.Adapter = _pagerAdapter;
			_viewPager.OffscreenPageLimit = 2;

			Observable.FromEventPattern<ViewPager.PageScrolledEventArgs> (
				x => _viewPager.PageScrolled += x,
				x => _viewPager.PageScrolled -= x)
			          .Select (args => args.EventArgs)
			          .StartWith(new ViewPager.PageScrolledEventArgs(0, 0, 0))
			          .PairWithPrevious()
			          .Skip(1)
			          .Select(args => {
					      var scrollDirection =
						      args.Item1.Position != args.Item2.Position
							      ? ScrollDirection.Center
							      : args.Item1.PositionOffset > args.Item2.PositionOffset
							          ? ScrollDirection.Left
							          : ScrollDirection.Right;
				          return new {
						      Current =  _pagerAdapter.GetItem(args.Item2.Position) as Walkthrough, 
						      Next = _pagerAdapter.GetItem(args.Item2.Position+1) as Walkthrough, 
							  args.Item2.PositionOffset, 
					          ScrollDirection = scrollDirection 
						  };
					  })
			          .Where(args => args.ScrollDirection == ScrollDirection.Center || args.Current != null && args.Next != null)
			          .Subscribe (args => {
			     	      if (args.ScrollDirection == ScrollDirection.Center) {
					          args.Current.View?.SetBackgroundColor (args.Current.BackgroundColor);
					      } else if (args.ScrollDirection == ScrollDirection.Left || args.ScrollDirection == ScrollDirection.Right) {
						      var color = args.Current.BackgroundColor.Lerp (args.Next.BackgroundColor, args.PositionOffset);
							  args.Current.View?.SetBackgroundColor (color);
						      args.Next.View?.SetBackgroundColor (color);
					      }	
					  })
			          .DisposeWith(ControlBindings);
		}
	}
}



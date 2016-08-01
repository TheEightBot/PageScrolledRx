using System;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace RxPageScrolled
{
	public static class IObservableExtensions
	{
		public static IObservable<Tuple<TSource, TSource>> PairWithPrevious<TSource>(this IObservable<TSource> source)
		{
			return source.Scan(
				Tuple.Create(default(TSource), default(TSource)),
				(acc, current) => Tuple.Create(acc.Item2, current));
		}

		public static TDisposable DisposeWith<TDisposable> (this TDisposable observable, Lazy<CompositeDisposable> disposables) where TDisposable : class, IDisposable
		{
			if (observable != null)
				disposables.Value.Add (observable);

			return observable;
		}
	}
}


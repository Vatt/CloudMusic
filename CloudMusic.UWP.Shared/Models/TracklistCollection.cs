using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.CoreLibrary;
using System.Collections.ObjectModel;

namespace CloudMusic.UWP.Models
{
    public class TracklistCollection : ObservableCollection<TrackViewModel>, ISupportIncrementalLoading
    {
        private CloudTracklist _original;

        public TracklistCollection() : base()
        {
        }

        public TracklistCollection(CloudTracklist list)
        {
            _original = list;
            foreach (var track in _original.ListData)
            {
                base.Add(new TrackViewModel(track));
            }
        }

        public bool HasMoreItems
        {
            get
            {
                return _original.HaveMore();
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var dispatcher = Window.Current.Dispatcher;
            return Task.Run(async () =>
            {
                try
                {
                    uint Length = 0;
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, InvokeLoadStarted);
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            var items = _original.LoadMoreIfPossible();
                            foreach (var item in items)
                            {
                                base.Add(new TrackViewModel(item));
                            }
                            Length = (uint)items.Count;
                        }
                    );
                    return new LoadMoreItemsResult { Count = (uint)Length };
                }
                catch(Exception)
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, InvokeLoadFailed);
                    return new LoadMoreItemsResult { Count= 0 };
                }
                finally
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, InvokeLoadCompleted);
                }
            }).AsAsyncOperation();
        }


        protected void InvokeLoadStarted()
        {
            LoadStarted?.Invoke();
        }

        protected void InvokeLoadCompleted()
        {
            LoadCompleted?.Invoke();
        }

        protected void InvokeLoadFailed()
        {
            LoadFailed?.Invoke();
        }

        public delegate void LoadCompletedEventHandler();

        public delegate void LoadFailedEventHandler();

        public delegate void LoadStartedEventHandler();

        public event LoadFailedEventHandler LoadFailed;

        public event LoadStartedEventHandler LoadStarted;

        public event LoadCompletedEventHandler LoadCompleted;

    }
}

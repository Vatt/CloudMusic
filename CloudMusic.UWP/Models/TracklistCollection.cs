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
        private int Index;
        public TracklistCollection() : base()
        {
            Index = 0;
        }

        public TracklistCollection(CloudTracklist list)
        {
            Index = 0;
            _original = list;
            foreach (var track in _original.ListData)
            {
                base.Add(new TrackViewModel(track, Index));
                Index++;
            }
        }

        public bool HasMoreItems
        {
            get
            {
                if (_original == null) return false;
                return _original.HaveMore();
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadMoreAsync(count).AsAsyncOperation();
        }


        private async Task<LoadMoreItemsResult> LoadMoreAsync(uint count)
        {
            try
            {
                var items = await _original.LoadMoreIfPossibleAsync();
                uint Length = 0;
                InvokeLoadStarted();

                foreach (var item in items)
                {
                    base.Add(new TrackViewModel(item, Index));
                    Index++;
                }
                Length = (uint)items.Count;

                return new LoadMoreItemsResult { Count = (uint)Length };
            }
            catch (Exception e)
            {
                InvokeLoadFailed();
                return new LoadMoreItemsResult { Count = 0 };
            }
            finally
            {
                InvokeLoadCompleted();
            }
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

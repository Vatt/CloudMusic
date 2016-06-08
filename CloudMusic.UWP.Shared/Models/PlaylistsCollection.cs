﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CloudMusic.UWP.ViewModels;
using CloudMusicLib.CoreLibrary;
using CloudMusicLib.ServiceCore;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Core;

namespace CloudMusic.UWP.Models
{
    public class PlaylistsCollection:ObservableCollection<PlaylistViewModel>, ISupportIncrementalLoading
    {
        public CloudPlaylistList _original;
        public PlaylistsCollection() : base()
        { }
        public PlaylistsCollection(CloudPlaylistList lists)
        {
            _original = lists;
            foreach (var cloudPlaylist in lists.ListData)
            {
                base.Add(new PlaylistViewModel(cloudPlaylist));
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
            var dispatcher = Window.Current.Dispatcher;
            return Task.Run(async () =>
            {
                try
                {
                    uint Length = 0;
                    var itemsTask = _original.LoadMoreIfPossibleAsync();
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, InvokeLoadStarted);
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        async() =>
                        {
                            var items = await itemsTask;
                            foreach (var item in items)
                            {
                                base.Add(new PlaylistViewModel(item));
                            }
                            Length = (uint)items.Count;
                        }
                    );
                    return new LoadMoreItemsResult { Count = (uint)Length };
                }
                catch (Exception)
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, InvokeLoadFailed);
                    return new LoadMoreItemsResult { Count = 0 };
                }
                finally
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, InvokeLoadCompleted);
                }
            }).AsAsyncOperation();
        }

        public override string ToString()
        {
            string data = "";
            foreach (var cloudPlaylist in _original.ListData)
            {
                data += cloudPlaylist.ToString();
            }
            if (_original == null) data = "PlaylistsCollection is null!";
            return data;
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

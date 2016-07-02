using CloudMusic.UWP.Common;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusic.UWP.ViewModels
{
    
    class PlayerControlViewModel : NotificationBase
    {
        private TrackViewModel _activeTrack;
        private TracklistCollection _activeTracklist;
        private Stack<int> _history;
        private int _tracklistLength;
        public TrackViewModel ActiveTrack
        {
            get
            {
                return _activeTrack;
            }
            set
            {
                ForcedSetProperty(ref _activeTrack, value);
                //  _activeTrack = value;

            }
        }
        public TracklistCollection ActiveTracklist
        {
            get
            {
                return _activeTracklist;
            }
            set
            {
                SetProperty(ref _activeTracklist, value);
            }
        }
        public bool IsShuffled { get; set; }
        public bool IsRepeated { get; set; }
        public bool IsPaused   { get; set; }
        public bool IsMuted    { get; set; }
        public PlayerControlViewModel()
        {
            GlobalEventSet.RegisterOrAdd("ActiveTrackChange", new Action<TrackViewModel>((track) => {
                if(ActiveTrack == track) { return; }
                 /*
                 * Если это первый клик в плейлисте в историю подается кликнутый трек,
                 * иначе подается прошлый активный
                 */             
                if (ActiveTrack == null)
                {
                    _history.Push(track.Index);
                }
                else
                {
                    _history.Push(ActiveTrack.Index);
                }
                ActiveTrack = track;
                IsPaused = false;
            }));
            GlobalEventSet.RegisterOrAdd("ActiveTracklistChange", new Action<TracklistCollection>((tracklist) =>
            {               
                
                if ( (_activeTracklist == null)||
                     ( _activeTracklist.GetHashCode() != tracklist.GetHashCode()) )                      
                {
                    _activeTracklist = tracklist;
                    _tracklistLength = _activeTracklist.Count;                    
                    _history.Clear();
                }
            }));
            IsMuted = false;
            _history = new Stack<int>();

        }
        public async void NextTrack()
        {
            if (_activeTracklist == null) {  return; }
            _tracklistLength = ActiveTracklist.Count;
            if(IsRepeated)
            {
                ActiveTrack = ActiveTrack;
                return;
            }
            if (IsShuffled&&!IsRepeated)
            {
                RandomNextTrack();
            }
            else if (!IsShuffled && !IsRepeated)
            {
                await LinearNextTrack();
            }
            
        }
        public async void ForcedNextTrack()
        {
            if (_activeTracklist == null) { return; }
            _tracklistLength = ActiveTracklist.Count;
            if (IsShuffled)
            {
                RandomNextTrack();
            }
            else
            {
                await LinearNextTrack();
            }

        }
        public void PrevTrack()
        {
            if (_activeTracklist == null) { return; }
            if (_history.Count == 0) { return; }
            int index = _history.Pop();
            ActiveTrack = _activeTracklist.ElementAt(index);
        }
        public void SwitchPlayPause()
        {
            IsPaused = !IsPaused;
        }

        private void RandomNextTrack()
        {
            Random rnd = new Random();
            int next = rnd.Next(_tracklistLength);
            _history.Push(_activeTrack.Index);
            var nextTrack = _activeTracklist.ElementAt(next);
            GlobalEventSet.Raise("ActiveTrackChange", nextTrack);
        }
        private async Task LinearNextTrack()
        {
            int next = _activeTrack.Index + 1;
            if (next >= _tracklistLength)
            {
                //любое число, аргумент не используется в функции
                await _activeTracklist.LoadMoreItemsAsync(0);
                _tracklistLength = _activeTracklist.Count;
                if (next >= _tracklistLength)
                {
                    next = 0;
                }
            }
            _history.Push(_activeTrack.Index);
            var nextTrack = _activeTracklist.ElementAt(next);
            GlobalEventSet.Raise("ActiveTrackChange", nextTrack);
        }

    }
}

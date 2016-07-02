using CloudMusic.UWP.Common;
using CloudMusic.UWP.Models;
using CloudMusic.UWP.ViewModels.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


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
        public void NextTrack()
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
                Random rnd = new Random();
                int next = rnd.Next(_tracklistLength);
                _history.Push(_activeTrack.Index);
                ActiveTrack = _activeTracklist.ElementAt(next);
            }else if (!IsShuffled && !IsRepeated)
            {
                int next = _activeTrack.Index + 1;
                _history.Push(_activeTrack.Index);
                ActiveTrack = _activeTracklist.ElementAt(next);
            }
            
        }
        public async void ForcedNextTrack()
        {
            if (_activeTracklist == null) { return; }
            _tracklistLength = ActiveTracklist.Count;
            if (IsShuffled)
            {
                Random rnd = new Random();
                int next = rnd.Next(_tracklistLength);
                _history.Push(_activeTrack.Index);
                ActiveTrack = _activeTracklist.ElementAt(next);
            }
            else
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
                ActiveTrack = _activeTracklist.ElementAt(next);
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

    }
}

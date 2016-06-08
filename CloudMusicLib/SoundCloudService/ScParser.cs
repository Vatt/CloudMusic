using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.CoreLibrary;
using Newtonsoft.Json.Linq;
using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.SoundCloudService
{
    public class ScParser
    {
        public static CloudTrack ParseTrackJson(JToken json)
        {
           /* if (json["streamable"]==null)
            {
                return null;
            }
            if (((string)json["streamable"]).Equals("false"))
            {
                return null;
            }
            */
            if ((string)json["stream_url"] == null)
            {
                return null;
            }
            var service = "SoundCloud";
            var name =   (string) json["title"];
            var artist = (string) json["username"];
            var source = new Uri(json["stream_url"] + "?client_id="+ ScApi.ScService.ClientId);
            Uri image = null;
            string imageStr = (string)json["artwork_url"];
            if (imageStr!=null)
            {
                image = new Uri(imageStr.Replace("large", "small"));
            }           
            CloudTrack track = new CloudTrack
            {
                ServiceSource = service,
                TrackName = name,
                ArtistName = artist,
                SourceData = source,
                TrackImage = image,
            };
            return track;
        }
        /*
        public static CloudPlaylist ParsePlaylistJson(JToken json)
        {
            CloudPlaylist playlist = new CloudPlaylist();
            CloudTracklist tracklist = new CloudTracklist(CloudListMode.Constant);
            playlist.ServiceSource = "SoundCloud";
            playlist.Name = (string)json["title"];
            //JArray.Parse(ScApi.GetPlaylistTracksJson((string)json["tracks_uri"])))
            foreach (var track in  json["tracks"])
            {
                var tryParse = ParseTrackJson(JObject.Parse(track.ToString()));
                if (tryParse != null)
                {
                    tracklist.ListData.Add(tryParse);
                }                
            }
            playlist.Data = tracklist;
            return playlist;
        }
        */
        public static CloudPlaylist ParsePlaylistJson(JToken json)
        {
            string tracksRefStr = json["tracks_uri"].ToString() + "?client_id=" + ScApi.ScService.ClientId;
            LazyLoad < CloudTracklist > lazyTracklist = new ScLazyTracklist(tracksRefStr);
            CloudPlaylist playlist = new CloudPlaylist(lazyTracklist);
           // CloudTracklist tracklist = new CloudTracklist(CloudListMode.Constant);
            playlist.ServiceSource = ScApi.ScService.ServiceName;
            playlist.Name = (string)json["title"];
            //JArray.Parse(await ScApi.GetPlaylistTracksJsonAsync((string)json["tracks_uri"])))
            //foreach (var track in  json["tracks"])
            //{
            //    var tryParse = ParseTrackJson(JObject.Parse(track.ToString()));
            //    if (tryParse != null)
            //    {
            //        tracklist.ListData.Add(tryParse);
            //    }
            //}
            //playlist.Data = tracklist;
              return playlist;
        }
       /* public static async Task<ScPlaylistsResult> ParsePlaylistsJsonAsync(JObject json)
        {
            List<CloudPlaylist> data = new List<CloudPlaylist>();
            JArray playlists = (JArray)json["collection"];
            string next = "";
            JToken tok;
            if (json.TryGetValue("next_href", out tok))
            {

                next = (string)json["next_href"];
            }
            foreach (var playlist in playlists)
            {
                data.Add(ParsePlaylistJson(playlist));
            }
            return new ScPlaylistsResult(ServiceCore.ResultType.Ok, data, next);
        }*/
        public static ScPlaylistsResult ParsePlaylistsJson(JObject json)
        {
            List<CloudPlaylist> data = new List<CloudPlaylist>();
            JArray playlists = (JArray)json["collection"];
            string next = "";
            JToken tok;
            if (json.TryGetValue("next_href", out tok))
            {

                next = (string)json["next_href"];
            }
            foreach (var playlist in playlists)
            {
                data.Add(ParsePlaylistJson(playlist));
            }
            return new ScPlaylistsResult(ServiceCore.ResultType.Ok, data, next);
        }

        public static ScTracksResult ParseTrackListJson(JObject json)
        {
            List<CloudTrack> tracklist = new List<CloudTrack>();
            JArray tracks = (JArray)json["collection"];
            string next = "";
            JToken tok;
            if (json.TryGetValue("next_href",out tok))
            { 

                next = (string)json["next_href"];
            }
            foreach (var track in tracks)
            {
                var tryParse = ParseTrackJson(track);
                if (tryParse!=null)
                {
                    tracklist.Add(tryParse);
                }
            }
            return new ScTracksResult(ServiceCore.ResultType.Ok,tracklist,next);
        }
    }
}

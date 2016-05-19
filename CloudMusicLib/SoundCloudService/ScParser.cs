using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.CoreLibrary;
using Newtonsoft.Json.Linq;

namespace CloudMusicLib.SoundCloudService
{
    public class ScParser
    {
        public static CloudTrack ParseTrackJson(JToken json)
        {
            //JObject json = jsonData;
            var service = "SoundCloud";
            var name =   (string) json["title"];
            var artist = (string) json["username"];
            var source = new Uri(json["stream_url"] + "?client_id="+ ScApi.ScService.ClientId);
            Uri image = null;
            string imageStr = json["artwork_url"].ToString();
            if (imageStr.Length > 0)
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

        public static CloudPlaylist ParsePlaylistJson(JToken json)
        {
            CloudPlaylist playlist = new CloudPlaylist();
            CloudTracklist tracklist = new CloudTracklist();
            playlist.ServiceSource = "SoundCloud";
            playlist.Name = (string)json["title"];
            foreach (var track in json["tracks"])
            {
                tracklist.TracklistData.Add(ParseTrackJson(JObject.Parse(track.ToString())));
            }
            playlist.Data = tracklist;
            return playlist;
        }

        public static IList<CloudPlaylist> ParsePlaylistsJson(JArray json)
        {
            IList<CloudPlaylist> data = new List<CloudPlaylist>();
            foreach (var playlist in json)
            {
                data.Add(ParsePlaylistJson(playlist));
            }
            return data;
        }

        public static CloudTracklist ParseTackListJson(JArray json)
        {
            CloudTracklist tracklist = new CloudTracklist();
            foreach (var track in json)
            {
                tracklist.TracklistData.Add(ParseTrackJson(track));
            }
            return tracklist;
        }
    }
}

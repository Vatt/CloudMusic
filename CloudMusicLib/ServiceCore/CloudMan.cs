using CloudMusicLib.CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMusicLib.ServiceCore
{
    public class CloudMan
    {
        private static Dictionary<string,CloudService> _services;

        static CloudMan()
        {
            _services = new Dictionary<string, CloudService>();
            RegisterService(new SoundCloudService.SoundCloudService());
            VerifyServices();
        }
        public static void RegisterService(CloudService service)
        {
            _services.Add(service.ServiceName,service);
        }

        public static IList<CloudService> Services()
        {
            return _services.Values.ToList();
        }
        private static void VerifyServices()
        {
            foreach(var service in _services.Values)
            {
                if (service.Connection==null)
                {
                    throw new NullReferenceException($"In {nameof(service)} connection in null");
                }
            }
        }
        public async static Task<Dictionary<string, ServiceResult<TOutType>>> InvokeCommandAsync<TOutType, TArgType>(ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            var data = new Dictionary<string, ServiceResult<TOutType>>();
            foreach (var service in _services.Values)
            {
                if(service.IsSupportedCommand(command))
                {
                    data.Add(service.ServiceName, await service._commands[command].InvokeAsync<TOutType, TArgType>(args));
                }
            }
            return data;
        }
        public static Dictionary<string, ServiceResult<TOutType>> InvokeCommand<TOutType, TArgType>(ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            var data = new Dictionary<string, ServiceResult<TOutType>>();
            foreach (var service in _services.Values)
            {
                if (service.IsSupportedCommand(command))
                {
                    data.Add(service.ServiceName,service._commands[command].Invoke<TOutType, TArgType>(args));
                }
            }
            return data;
        }

        public static async  Task<ServiceResult<TOutType>> InvokeCommandAsync<TOutType, TArgType>(string serviceName, ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            ServiceResult<TOutType> result = new ServiceResult<TOutType>(serviceName, ResultType.Err, null);
            if (_services[serviceName].IsSupportedCommand(command))
            {
                return await _services[serviceName]._commands[command].InvokeAsync<TOutType, TArgType>(args);
            }
            return result;
        }
        public static ServiceResult<TOutType> InvokeCommand<TOutType, TArgType>(string serviceName, ServiceCommands command, params TArgType[] args) where TOutType : class
        {
            ServiceResult<TOutType>  result = new ServiceResult<TOutType>(serviceName,ResultType.Err,null);
            if (_services[serviceName].IsSupportedCommand(command))
            {
                return  _services[serviceName]._commands[command].Invoke<TOutType, TArgType>(args);
            }
            return result;
        }

        public async static Task<CloudTracklist> SearchTracksAsync(string template)
        {
            var tracklist = new CloudTracklist(CloudListMode.Dynamic);
            var data = await InvokeCommandAsync<List<CloudTrack>, string>(ServiceCommands.SearchByTracks, template);
            foreach (var result in data)
            {
                if (result.Value.Type == ResultType.Ok)
                {
                    tracklist.MergeOther(result.Value as ServiceResultCollection<CloudTrack>);
                }
            }
            return tracklist;
        }
        public static CloudTracklist SearchTracks(string template)
        {
            var tracklist = new CloudTracklist(CloudListMode.Dynamic);
            var data = InvokeCommand<List<CloudTrack>, string>(ServiceCommands.SearchByTracks, template);
            foreach (var result in data)
            {
                if (result.Value.Type == ResultType.Ok)
                {
                    tracklist.MergeOther(result.Value as ServiceResultCollection<CloudTrack>);
                }
            }
            return tracklist;
        }
        public static async Task<CloudPlaylistList> SearchPlaylistsAsync(string template)
        {
            var playlists = new CloudPlaylistList(CloudListMode.Dynamic);
            var data = await InvokeCommandAsync<List<CloudPlaylist>, string>(ServiceCommands.SearchByPlaylists, template);
            foreach (var result in data)
            {
                if (result.Value.Type == ResultType.Ok) { 
                    playlists.MergeOther(result.Value as ServiceResultCollection<CloudPlaylist>);
                }
            }
            return playlists;
        }
        public static CloudPlaylistList SearchPlaylists(string template)
        {
            var playlists = new CloudPlaylistList(CloudListMode.Dynamic);
            var data = InvokeCommand<List<CloudPlaylist>, string>(ServiceCommands.SearchByPlaylists, template);
            foreach (var result in data)
            {
                if (result.Value.Type == ResultType.Ok)
                {
                    playlists.MergeOther(result.Value as ServiceResultCollection<CloudPlaylist>);
                }
            }
            return playlists;
        }

        public static async Task<CloudPlaylistList> GetUserPlaylistsAsync()
        {
            var playlists = new CloudPlaylistList(CloudListMode.Dynamic);
            var data = await InvokeCommandAsync<List<CloudPlaylist>, DummyArgType>(ServiceCommands.GetUserPlaylists);
            foreach (var result in data)
            {
                if (result.Value.Type == ResultType.Ok)
                {
                    playlists.MergeOther(result.Value as ServiceResultCollection<CloudPlaylist>);
                }
            }
            return playlists;
        }
        public static CloudPlaylistList GetUserPlaylists()
        {
            var playlists = new CloudPlaylistList(CloudListMode.Dynamic);
            var data = InvokeCommand<List<CloudPlaylist>, DummyArgType>(ServiceCommands.GetUserPlaylists);
            foreach (var result in data)
            {
                if (result.Value.Type == ResultType.Ok)
                {
                    playlists.MergeOther(result.Value as ServiceResultCollection<CloudPlaylist>);
                }
            }
            return playlists;
        }

    }
}

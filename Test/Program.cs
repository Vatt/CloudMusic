using System;
using System.Collections.Generic;
using CloudMusicLib.ServiceCore;
using CloudMusicLib.SoundCloudService;
using  System.Linq;
using CloudMusicLib.CoreLibrary;
using Newtonsoft.Json.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static async void Test()
        {
          //  CloudMan.RegisterService(new SoundCloudService());
          
            
            var some = CloudMan.InvokeCommand<DummyOutType, string>("SoundCloud", ServiceCommands.Init,
                                                                    "109f016fa8b98246e0e5156074389ff1",
                                                                    "08b584be83dd9825488004bcee50e3b6");
            SoundCloudService scService = ScApi.ScService;
            await CloudMan.InvokeCommandAsync<DummyOutType, string>("SoundCloud", ServiceCommands.Authorization,
                                                                     "gamover-90@hotmail.com", "gam2106");

            Console.WriteLine(scService.Connection.ToString());
            var mePlaylists = await  CloudMan.InvokeCommandAsync<IList<CloudPlaylist>,DummyArgType>("SoundCloud",ServiceCommands.GetUserPlaylists);

            foreach (var cloudPlaylist in mePlaylists)
            {
                Console.WriteLine(cloudPlaylist.ToString());
            }
            var SearchData =
                await
                    CloudMan.SearchTracksAsync("Seceqtrique");

            Console.WriteLine("Search by Seceqtrique: ");
            Console.WriteLine(SearchData.ToString());

        }

    }
    
}


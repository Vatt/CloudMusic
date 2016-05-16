using System;
using CloudMusicLib.ServiceCore;
using CloudMusicLib.SoundCloudService;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        static async void Test()
        {
            CloudMan.RegisterService(new SoundCloudService());
            var some = CloudMan.InvokeCommand<string, string>("SoundCloud", ServiceCommands.Init,
                                                              "109f016fa8b98246e0e5156074389ff1",
                                                              "08b584be83dd9825488004bcee50e3b6");

            var data =  await  CloudMan.InvokeCommandAsync<string, string>("SoundCloud", ServiceCommands.Authorization,
                                                                           "gamover-90@hotmail.com", "gam2106");

            Console.WriteLine(data);
          //  var MePlaylist = await CloudMan.InvokeCommandAsync<string, string>("SoundCloud",ServiceCommands.GetUserPlaylists);
        }

    }
    
}


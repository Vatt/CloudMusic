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
            Console.ReadKey();
        }

        static async void Test()
        {
            SoundCloudService ScService = new SoundCloudService();
            CloudMan.RegisterService(ScService);
            var some = CloudMan.InvokeCommand<DummyOutType, string>("SoundCloud", ServiceCommands.Init,
                                                                    "109f016fa8b98246e0e5156074389ff1",
                                                                    "08b584be83dd9825488004bcee50e3b6");

            await  CloudMan.InvokeCommandAsync<DummyOutType, string>("SoundCloud", ServiceCommands.Authorization,
                                                                     "gamover-90@hotmail.com", "gam2106");

            Console.WriteLine(ScService.Connection.ToString());
            var mePlaylist = await  CloudMan.InvokeCommandAsync<string,DummyArgType>("SoundCloud",ServiceCommands.GetUserPlaylists);
            Console.WriteLine(mePlaylist);
        }

    }
    
}


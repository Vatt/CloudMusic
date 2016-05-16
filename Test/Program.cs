using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

            var data =   CloudMan.InvokeCommand<string, string>("SoundCloud", ServiceCommands.Authorization,
                                                                 "gamover-90@hotmail.com", "gam2106");

            Writer(data);
        }

        static void Writer(string str)
        {
            Console.WriteLine(str);
        }
    }
    
}


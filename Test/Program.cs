using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudMusicLib.SoundCloudService;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SoundCloudService soundcloud = new SoundCloudService();
            soundcloud.Authorization("protossgamover@gmail.com", "gam2106");
        }
    }
}

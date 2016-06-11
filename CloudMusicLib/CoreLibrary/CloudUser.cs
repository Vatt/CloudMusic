using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CloudUser()
        {
            UserName = "";FirstName = "";LastName = "";
        }
        public override string ToString()
        {
            string data = $"{UserName}";
            if (FirstName.Length==0 && LastName.Length==0)
            {
                return data;
            }
            data = $"{data}({FirstName} {LastName}";
            return base.ToString();
        }
    }
}

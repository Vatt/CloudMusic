using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudUser
    {
        public string Login { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
        public CloudUser(string login)
        {
            Login = login;
            UserName = "";
            FirstName = "";
            LastName = "";
            Id = "";
        }
        public override string ToString()
        {
            string data = "";
            if (UserName.Length >0 )
            {
                data = $"{UserName}";
            }
            else
            {
                data = $"{Login}";
            }
            if (FirstName.Length==0 && LastName.Length==0)
            {
                return data;
            }
            data = $"{data}({FirstName} {LastName})";
            return data;
        }
    }
}

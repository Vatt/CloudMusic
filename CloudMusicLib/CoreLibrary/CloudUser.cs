using CloudMusicLib.ServiceCore;

namespace CloudMusicLib.CoreLibrary
{
    public class CloudUser
    {
        CloudService owner;

        public string Login { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
        public CloudUser(CloudService service,string login)
        {
            this.owner = service;
            Login = login;
            UserName = "";
            FirstName = "";
            LastName = "";
            Id = "";
        }
        //public CloudUser(CloudService service)
        //{
        //    this.owner = service;
        //    Login = "";
        //    UserName = "";
        //    FirstName = "";
        //    LastName = "";
        //    Id = "";
        //}
        public override string ToString()
        {
            string data = "";
            if (UserName?.Length >0 )
            {
                data = $"{UserName}";
            }
            else
            {
                data = $"{Login}";
            }
            if (FirstName?.Length==0 && LastName?.Length==0)
            {
                return data;
            }
            data = $"{data}({FirstName} {LastName})";
            return data;
        }
    }
}

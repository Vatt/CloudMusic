namespace CloudMusicLib.ServiceCore
{
    public interface ICloudService
    {
        string ServiceName { get;}
        bool IsAuthorizationRequired{ get; }
        bool Authorization(string user, string password);
        
    }
}

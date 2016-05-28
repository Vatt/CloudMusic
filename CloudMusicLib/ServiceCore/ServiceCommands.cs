

namespace CloudMusicLib.ServiceCore
{
    public enum ServiceCommands :byte
    {

        Init,
        Authorization,

        SearchByTracks,
        SearchByArtists,
        SearchByAlbums,
        SearchByPlaylists,

        GetUserPlaylists,
        GetUserTraks,
    }
}

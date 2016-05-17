

namespace CloudMusicLib.ServiceCore
{
    public enum ServiceCommands :byte
    {

        Init,
        Authorization,

        FullSearch,
        SearchByTracks,
        SearchByArtists,
        SearchByAlbums,

        GetUserPlaylists,
        GetUserTraks,
    }
}

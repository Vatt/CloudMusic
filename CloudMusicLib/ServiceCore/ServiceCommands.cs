

namespace CloudMusicLib.ServiceCore
{
    internal enum ServiceCommands :byte
    {
        Authorization,

        FullSearch,
        SearchByTracks,
        SearchByArtists,
        SearchByAlbums,

        GetUserPlaylists,
    }
}

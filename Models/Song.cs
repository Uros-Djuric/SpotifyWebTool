using System.ComponentModel;

namespace SpotifyApp.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? SpotifyId { get; set; }
        [DisplayName("Song Name")]
        public string? SongName { get; set; }
        [DisplayName("Artist Name ")]
        public string? ArtistName { get; set; }
        [DisplayName("Display Name")]
        public bool SaveSong { get; set; }
    }
}

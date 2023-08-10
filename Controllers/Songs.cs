using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyApp.Models;
using System.Text.Json.Serialization;

namespace SpotifyApp.Controllers
{
    public class Songs : Controller
    {
        private readonly SpotifyAPI.Web.SpotifyClient spotifyClient = new SpotifyClient("BQCpwQaLtHXrmtelUR7a9F45GlNGDxOj6N9jz58wna8AW82dNPgATxlsIL-Q-7C2qHB8cI5LUg5HuyXnMIiNGqw560ppG0dH9a4injNGtu0yhN9rIdaS4Dn4hNRhwRB3zQ6HP53GsTP7wXov3aa4Pv6_JMEkjRtcKcsAFhyupQJ-m-IhrX8t4J_WBed8AJHNArU_xqI0GXBXPkxfY14AWS1CHdEbzFcW8gYklfZQQXLcj0fwqFFFUxzLCFxbVL2Sm0fEdqr85I-U7IjXMRoRhS1OV7luW_yYcLb5BpvQgd_GZ-XXNWM");
        public async Task<IActionResult> IndexAsync()
        {
            
            var recently_played = await spotifyClient.Player.GetRecentlyPlayed();

            //var ArtistName = recently_played.Items[0].Track.Artists[0].Name;

            var recently_played_list = from song in recently_played.Items
                                       select new Song()
                                       {
                                           SongName = song.Track.Name,
                                           ArtistName = song.Track.Artists[0].Name,
                                           SpotifyId = song.Track.Id
                                       };

            return View(recently_played_list);
        }

        public async Task<IActionResult> SaveSongAsync(string? songname)
        {
            var recently_played = await spotifyClient.Player.GetRecentlyPlayed();
            await spotifyClient.Library.SaveTracks(new LibrarySaveTracksRequest(new List<string> { songname }));
                


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string? serializedReturnList)
        {
            if (serializedReturnList == null)
            {
                return View(new List<Song>() { });
            }
            else {
                return View(JsonConvert.DeserializeObject<List<Song>>(serializedReturnList));
            }
        }

        public async Task<IActionResult> SearchSong(string? searchItem) {
            SearchRequest request = new SearchRequest(SearchRequest.Types.Track | SearchRequest.Types.Artist, searchItem);
            var searchResult = await spotifyClient.Search.Item(new SearchRequest(SearchRequest.Types.Track | SearchRequest.Types.Artist, searchItem));

            var searchResultTracks = from i in Enumerable.Range(0, 10) select new Song()
            {
                SongName = searchResult.Tracks.Items[i].Name,
                ArtistName = searchResult.Tracks.Items[i].Artists[0].Name,
                SpotifyId = searchResult.Tracks.Items[i].Id
            };

            var searchResultArtist = from i in Enumerable.Range(0, 10) select new Song()
            {
                SongName = searchResult.Artists.Items[i].Name,
                ArtistName = searchItem,
                SpotifyId = searchResult.Artists.Items[i].Id
            };

            string serializedReturnList = JsonConvert.SerializeObject(searchResultTracks.ToList().Concat(searchResultArtist.ToList()).ToList());

            return RedirectToAction(nameof(Search), new {serializedReturnList = serializedReturnList});
            
        }
    }
}

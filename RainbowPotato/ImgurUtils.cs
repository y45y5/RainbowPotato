using DSharpPlus.Entities;
using Newtonsoft.Json.Linq;

namespace RainbowPotato
{
    internal class ImgurUtils
    {
        private Dictionary<string, List<string>> availableImgurAlbums = new Dictionary<string, List<string>>();

        public async Task<DiscordEmbed> GetRandomImgurAlbumResultAsEmbed(string albumHash)
        {
            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.AddImage(await GetRandomElementFromImgurAlbum(albumHash));
            embedBuilder.AddDescription(":camera_with_flash: :sparkles:");

            return embedBuilder.Build();
        }

        public async Task<string> GetRandomElementFromImgurAlbum(string albumHash)
        {
            List<string> imgurAlbum = await GetAllImgurAlbumElements(albumHash);

            return imgurAlbum[new Random().Next(imgurAlbum.Count)];
        }

        private async Task<List<string>> GetAllImgurAlbumElements(string albumHash)
        {
            try
            {
                return availableImgurAlbums[albumHash];
            }
            catch 
            {
                await AddNewAvailableImgurAlbum(albumHash);

                return await GetAllImgurAlbumElements(albumHash);
            }
        }

        private async Task<string>AddNewAvailableImgurAlbum(string albumHash)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 5c8e6cfbd694564857a073884282770c3e4ef1a0");

                using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://api.imgur.com/3/album/{albumHash}/images"))
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string responseData = await httpResponseMessage.Content.ReadAsStringAsync();
                        availableImgurAlbums[albumHash] = ConvertJsonResponseToList(JObject.Parse(responseData));
                    }
                    else
                    {
                        // TODO ???
                    }
                }
            }

            return String.Empty;
        }

        private List<string> ConvertJsonResponseToList(JObject jsonResponse) 
        {
            List<string> availableImgurAlbums = new List<string>();
            foreach (JObject data in jsonResponse["data"])
            {
                availableImgurAlbums.Add(data["link"].ToString());
            }

            return availableImgurAlbums;
        }
    }
}

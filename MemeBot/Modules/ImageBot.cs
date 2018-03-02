using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MemeBot.Modules
{
    [Group("image")]
    public class ImageBot : ModuleBase
    {
        [Command, Summary("Gets the top image from google")]
        public async Task GetImageFromGoogle(string imageToSearch)
        {
            string HTMLData = await GetHTMLCode($"https://www.google.com/search?q={imageToSearch}&tbm=isch");
            List<string> imgUrls = GetImageUrls(HTMLData);
            byte[] rawImage = await imageBits(imgUrls[0]);
            MemoryStream imageStream = new MemoryStream(rawImage);

            await Context.Channel.SendFileAsync(imageStream, "requested-image.png");
        }

        private async Task<string> GetHTMLCode(string url)
        {
            string HTMLData;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            Stream responseData = response.GetResponseStream();

            if (responseData != null)
            {
                StreamReader streamReader = new StreamReader(responseData);

                HTMLData = await streamReader.ReadToEndAsync();

                return HTMLData;
            }

            
                return string.Empty;
            
        }

        private List<string> GetImageUrls(string HTMLData)
        {
            List<string> imgurls = new List<string>();
            int index = HTMLData.IndexOf("\"ou\"");

            while (index != -1)
            {
                index = HTMLData.IndexOf("\"", index + 4);
                index++;
                int index2 = HTMLData.IndexOf("\"", index);
                string imgurl = HTMLData.Substring(index, index2 - index);
                imgurls.Add(imgurl);
                index = HTMLData.IndexOf("\"ou\"", index2);
            }

            return imgurls;
        }
        private async Task<byte[]> imageBits(string imageURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageURL);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            Stream responseData = response.GetResponseStream();

            if (responseData != null)
            {
                BinaryReader binaryReader = new BinaryReader(responseData);

                byte[] bytes = binaryReader.ReadBytes(100000000);
                return bytes;
            }

            return null;
        }
    }
}

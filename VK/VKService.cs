using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VK
{

    public class VKService : IService
    {
        private string UrlGetVideo = "https://api.vk.com/method/{0}?owner_id={1}&count={2}&access_token={3}";
        private string UrlGetVideoOffset = "https://api.vk.com/method/{0}?owner_id={1}&count={2}&offset={3}&access_token={4}";
        WebClient client;

        public VKService()
        {
            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
        }
        public string GetVideo(int ownerId, int count,string access_token, int offset = 0)
        {          
            String jsonStringVideo;
            if (offset == 0)
                jsonStringVideo = client.DownloadString(String.Format(UrlGetVideo, "video.get", ownerId, count, access_token));
            else
                jsonStringVideo = client.DownloadString(String.Format(UrlGetVideoOffset, "video.get", ownerId, count, offset, access_token));
            return jsonStringVideo;
        }
    }
}

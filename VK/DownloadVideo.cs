﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace VK
{
    public class DownloadVideo
    {
        public static VideoCollection Load(int userId) {
            int index = 0;
            int MaxCountVideoForLoad = 200;
            int offset = -1;
            String urlGetVideo = "https://api.vk.com/method/video.get?owner_id=" + userId + "&count=" + MaxCountVideoForLoad + "&access_token=" + Program.AccessToken;
            String urlGetVideoOffset = "https://api.vk.com/method/video.get?owner_id=" + userId + "&count=" + MaxCountVideoForLoad + "&offset=" + offset + "&access_token=" + Program.AccessToken;
            VideoCollection videoCollection = new VideoCollection();
            WebClient client = new WebClient();
            String jsonStringVideo;
            videoCollection.CountVideo = 0;

            while (offset != videoCollection.CountVideo)
            {
                if (videoCollection.ArrayVideo == null) jsonStringVideo = client.DownloadString(urlGetVideo);
                else jsonStringVideo = client.DownloadString(urlGetVideoOffset);

                if (jsonStringVideo.Contains("error")) break;

                int indexBegin = jsonStringVideo.IndexOf('[');
                int indexEnd = jsonStringVideo.IndexOf(',');
                if (indexBegin != -1 && indexEnd != -1)
                {
                    if (videoCollection.ArrayVideo == null)
                        videoCollection.CountVideo = Int32.Parse(jsonStringVideo.Substring(indexBegin + 1, indexEnd - indexBegin - 1));
                    jsonStringVideo = jsonStringVideo.Remove(indexBegin + 1, indexEnd - indexBegin);
                    if (videoCollection.ArrayVideo == null)
                        videoCollection.ArrayVideo = JsonConvert.DeserializeObject<VideoCollection>(jsonStringVideo).ArrayVideo;
                    else
                    {
                        videoCollection.ArrayVideo.AddRange(JsonConvert.DeserializeObject<VideoCollection>(jsonStringVideo).ArrayVideo);
                    }
                    index++;
                    offset = videoCollection.ArrayVideo.Count();
                }        
            }
            System.Threading.Thread.Sleep(1000);
            return videoCollection;
        }
    }
}

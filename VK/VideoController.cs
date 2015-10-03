using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace VK
{
     class VideoController : VideoCollection
    {
         private int MaxCountVideoForLoad = 200;
         private VideoCollection VideoCollection = new VideoCollection();
         private String URLGetVideo = "https://api.vk.com/method/video.get?owner_id=72813887&count=200&access_token=" + Program.AccessToken;
         private String URLGetVideoOffset = "https://api.vk.com/method/video.get?owner_id=72813887&count=200&offset=";
         public void VideoLoad()
         {
             int Index = 0;

             WebRequest GetURL = WebRequest.Create(URLGetVideo);
             Stream StreamGetURL = GetURL.GetResponse().GetResponseStream();
             StreamReader Reader = new StreamReader(StreamGetURL);
             String JsonStringVideo = Reader.ReadToEnd();
             int IndexBegin = JsonStringVideo.IndexOf('[');
             int IndexEnd = JsonStringVideo.IndexOf(',');
             String CountVideo = JsonStringVideo.Substring(IndexBegin + 1, IndexEnd - IndexBegin - 1);
             VideoCollection.CountVideo = Int32.Parse(CountVideo);
             JsonStringVideo = JsonStringVideo.Remove(IndexBegin + 1, IndexEnd - IndexBegin);
             Video[] response = JsonConvert.DeserializeObject<VideoCollection>(JsonStringVideo).ArrayVideo;
             response.CopyTo(VideoCollection.ArrayVideo, Index);
             Array.Clear(response, 0, response.Count());
             Index++;
             while (VideoCollection.CountVideo > Index * MaxCountVideoForLoad)
             {
                 GetURL = WebRequest.Create(URLGetVideoOffset + Index * MaxCountVideoForLoad + "&access_token=" + Program.AccessToken);
                 StreamGetURL = GetURL.GetResponse().GetResponseStream();
                 Reader = new StreamReader(StreamGetURL);
                 JsonStringVideo = Reader.ReadToEnd();
                 IndexBegin = JsonStringVideo.IndexOf('[');
                 IndexEnd = JsonStringVideo.IndexOf(',');
                 JsonStringVideo = JsonStringVideo.Remove(IndexBegin + 1, IndexEnd - IndexBegin);
                 response = JsonConvert.DeserializeObject<VideoCollection>(JsonStringVideo).ArrayVideo;
                 response.CopyTo(VideoCollection.ArrayVideo, Index * MaxCountVideoForLoad);
                 Array.Clear(response, 0, response.Count());
                 Index++;
             }
         }
        public int FindPopularVideo() 
        {
            int MaxCountViews = 0;
            int IndexVideoMaxCountViews = 0;
            int Index = 0;
            foreach (var Video in VideoCollection.ArrayVideo)
            {
                if (Video.views > MaxCountViews)
                {
                    MaxCountViews = Video.views;
                    IndexVideoMaxCountViews = Index;
                }
                Index++;
            }
            return IndexVideoMaxCountViews;
        }
    }
}

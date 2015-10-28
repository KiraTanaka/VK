using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK
{
    public class Control
    {
      
        public static void ParseUrl(string url)
        {
            var urlParams = System.Web.HttpUtility.ParseQueryString(url);
            Program.AccessToken = urlParams.Get("access_token");
            Program.UserId = urlParams.Get("user_id");
        }
        public static List<VideoCollection> FillingListVideoCollection(List<VideoCollection> listVideoCollection,List<int> persons) {
            foreach(var person in persons){
                VideoCollection videoCollection= DownloadVideo.Load(person);
                if (!(videoCollection.ListVideo == null))
                    listVideoCollection.Add(videoCollection);
            }
            return listVideoCollection;
        }
        public static void Master() {
            PersonsID persons = new PersonsID();
            persons = DownloadFriends.Load();
            if (persons.Equals(null)) return;
            persons.PersonsId.Add(Int32.Parse(Program.UserId));
            List<VideoCollection> listVideoCollection=new List<VideoCollection>();
            FillingListVideoCollection(listVideoCollection,persons.PersonsId);
            
            Video VideoMaxCountViews = FindPopularVideoFriends(listVideoCollection);
            if (VideoMaxCountViews != null)
                System.Diagnostics.Process.Start("https://vk.com/video" + VideoMaxCountViews.owner_id + "_" + VideoMaxCountViews.vid);
            else
                System.Diagnostics.Process.Start("http://costper.ru/wp-content/uploads/2015/07/20244247-1748x984.jpg");
        }
        public static Video FindPopularVideoFriends(List<VideoCollection> listVideoCollection) {
            Video current;
            Video mostPopularVideo=null;
            foreach (var videoCollection in listVideoCollection) {
                if (!(videoCollection.ListVideo==null))
                {
                    current = videoCollection.ListVideo.Where(x=>x!=null).OrderByDescending(x => x.views).First();
                    if (mostPopularVideo==null || mostPopularVideo.views < current.views )
                        mostPopularVideo = current;
                }
            }
            return mostPopularVideo;
        }
    }
}

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
        public static void Master() {
            PersonsID persons = new PersonsID();
            persons = DownloadFriends.Load();
            if (persons.Equals(null)) return;
            persons.PersonsId.Add(Int32.Parse(Program.UserId));
            string VideoMaxCountViews = FindPopularVideoFriends(persons.PersonsId);

            System.Diagnostics.Process.Start("https://vk.com/video" + VideoMaxCountViews);
        }
        public static string FindPopularVideoFriends(List<int> persons) {
            int maxCountViews = 0;
            int idVideoMaxCountViews = 0;
            int ownerIdVideoMaxCountViews = 0;
            int indexPerson=0;
            int index=0;
            foreach (var person in persons) {
                int indexVideo=0;
                VideoCollection videoCollection = new VideoCollection();
                videoCollection = DownloadVideo.Load(person);
                if (!(videoCollection.ArrayVideo==null))
                {
                    indexVideo = FindPopularVideo(videoCollection);
                    if (maxCountViews < videoCollection.ArrayVideo.ElementAt(indexVideo).views)
                    {
                        maxCountViews = videoCollection.ArrayVideo.ElementAt(indexVideo).views;
                        idVideoMaxCountViews = videoCollection.ArrayVideo.ElementAt(indexVideo).vid;
                        ownerIdVideoMaxCountViews = videoCollection.ArrayVideo.ElementAt(indexVideo).owner_id;
                        indexPerson = index;
                    }
                }
                index++;
            }
            return ownerIdVideoMaxCountViews.ToString()+"_"+idVideoMaxCountViews.ToString();

        }
        public static int FindPopularVideo(VideoCollection videoCollection)
        {
            int maxCountViews = 0;
            int indexVideoMaxCountViews = 0;
            int index = 0;
            foreach (var Video in videoCollection.ArrayVideo)
            {
                if (Video.views > maxCountViews)
                {
                    maxCountViews = Video.views;
                    indexVideoMaxCountViews = index;
                }
                index++;
            }
            return indexVideoMaxCountViews;
        }
    }
}

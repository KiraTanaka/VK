using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

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
                System.Threading.Thread.Sleep(2000);
            }
            return listVideoCollection;
        }
        public static void Master() {
            List<Person> people = new List<Person>();
            List<int> personsId = new List<int>();
            List<VideoCollection> listVideoCollection = new List<VideoCollection>();
            Video mostPopularVideo;
            using (VKContext db = new VKContext())
            {
                people = db.People.ToList();
                personsId = null;
                if (people.Count != 0)
                {
                    foreach (var person in people)
                    {
                        personsId.Add(person.UID);
                    }

                    if (personsId.Count != 0)
                    {
                        listVideoCollection = null;
                        FillingListVideoCollection(listVideoCollection, personsId);
                        mostPopularVideo = FindPopularVideoFriends(listVideoCollection);
                        if (mostPopularVideo != null)
                        {
                            mostPopularVideo.DateTime = DateTime.Now;
                            db.PopularVideo.Add(mostPopularVideo);
                            db.SaveChanges();
                            System.Diagnostics.Process.Start("https://vk.com/video" + mostPopularVideo.OwnerId + "_" + mostPopularVideo.Vid);
                        }
                        else
                            System.Diagnostics.Process.Start("http://costper.ru/wp-content/uploads/2015/07/20244247-1748x984.jpg");
                    }
                }
            }
        }
        public static Video FindPopularVideoFriends(List<VideoCollection> listVideoCollection) {
            Video current;
            Video mostPopularVideo=null;
            foreach (var videoCollection in listVideoCollection) {
                if (!(videoCollection.ListVideo==null))
                {
                    current = videoCollection.ListVideo.Where(x=>x!=null).OrderByDescending(x => x.Views).First();
                    if (mostPopularVideo==null || mostPopularVideo.Views < current.Views )
                        mostPopularVideo = current;
                }
            }
            return mostPopularVideo;
        }
    }
}

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
        private DownloadVideo downloadVideo;
        public Control(IService service)
        {
            downloadVideo = new DownloadVideo(service);
        }
        public void ReceiveToken()
        {
            using (VKContext db = new VKContext())
            {
                Program.AccessToken = db.AccessToken.OrderByDescending(x=>x.Id).First().AccessToken;
            }
        }
        public List<Video> FillingListVideo(List<int> persons) {
            List<Video> listVideo = new List<Video>();
            foreach(var personId in persons){
                VideoCollection videoCollection = downloadVideo.Load(personId);
                if (!(videoCollection.ListVideo == null))
                    if (listVideo.Count != 0)
                        listVideo.AddRange(videoCollection.ListVideo);
                    else
                        listVideo = videoCollection.ListVideo;
                System.Threading.Thread.Sleep(2000);
            }
            return listVideo;
        }
        public void Master() {
            List<Person> people = new List<Person>();     
           // Video mostPopularVideo;
            using (VKContext db = new VKContext())
            {
                List<Video> listVideo;
                List<int> personsId = new List<int>();
                people = db.People.ToList();
                if (people.Count != 0)
                {
                    foreach (var person in people)
                    {
                        personsId.Add(person.UID);
                    }

                    if (personsId.Count != 0)
                    {
                        listVideo = FillingListVideo(personsId);
                        List<Video> topVideo = FindTop10Video(listVideo);
                        //mostPopularVideo = FindPopularVideo(listVideo);
                        if (topVideo.Count != 0)
                        {
                            foreach (var video in topVideo)
                            {
                                video.DateTime = DateTime.Now;
                                video.VKPlayer = "https://vk.com/video" + video.OwnerId + "_" + video.Vid;
                                db.PopularVideo.Add(video);
                            }                            
                            db.SaveChanges();
                            //System.Diagnostics.Process.Start(topVideo[0].VKPlayer);
                        }
                        //else
                        //    System.Diagnostics.Process.Start("http://costper.ru/wp-content/uploads/2015/07/20244247-1748x984.jpg");
                    }
                }
            }
        }
        public List<Video> FindTop10Video(List<Video> listVideo)
        {
            if (listVideo.Count == 0) return null;
            List<Video> topVideo = listVideo.Where(x=>x!=null).OrderByDescending(x => x.Views).Take(10).ToList();
            return topVideo;
        }      
    }
}

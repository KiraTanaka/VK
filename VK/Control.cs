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
        private DownloadVideo DownloadVideo;
        private List<Video> listVideo = new List<Video>();
        public Control(IVideoService service)
        {
            DownloadVideo = new DownloadVideo(service);
        }
        public void ReceiveToken()
        {
            using (VKContext db = new VKContext())
            {
                Program.AccessToken = db.AccessToken.OrderByDescending(x=>x.Id).First().AccessToken;
            }
        }
        public void FillingListVideo(List<Person> people) {
            foreach(var person in people){
                VideoCollection videoCollection = DownloadVideo.Load(person.UID);
                if (!(videoCollection.ListVideo == null))
                {
                    if (listVideo.Count != 0)
                        listVideo.AddRange(videoCollection.ListVideo);
                    else
                        listVideo = videoCollection.ListVideo;
                }
                System.Threading.Thread.Sleep(2000);
            }
        }
        public void Master() {
            List<Person> people = new List<Person>();
            using (VKContext db = new VKContext())
            {
                people = db.People.ToList();
                if (people.Count != 0)
                {
                    FillingListVideo(people);
                    List<Video> topVideo = FindTop10Video(listVideo);
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
        public List<Video> FindTop10Video(List<Video> listVideo)
        {
            if (listVideo.Count == 0) return null;
            List<Video> topVideo = listVideo.Where(x=>x!=null).OrderByDescending(x => x.Views).Take(10).ToList();
            return topVideo;
        }      
    }
}

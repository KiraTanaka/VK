﻿using System;
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
        public static void ReceiveUrl()
        {
            using (VKContext db = new VKContext())
            {
                Program.AccessToken = db.AccessToken.OrderByDescending(x=>x.Id).First().AccessToken;
                Program.UserId = "72813887";//urlParams.Get("user_id");
            }
        }
        public static List<Video> FillingListVideo(List<Video> listVideo,List<int> persons) {
            foreach(var person in persons){
                VideoCollection videoCollection= DownloadVideo.Load(person);
                if (!(videoCollection.ListVideo == null))
                    if (listVideo.Count != 0)
                        listVideo.AddRange(videoCollection.ListVideo);
                    else
                        listVideo = videoCollection.ListVideo;
                System.Threading.Thread.Sleep(2000);
            }
            return listVideo;
        }
        public static void Master() {
            List<Person> people = new List<Person>();     
           // Video mostPopularVideo;
            using (VKContext db = new VKContext())
            {
                List<Video> listVideo = new List<Video>();
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
                        listVideo = FillingListVideo(listVideo, personsId);
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
                            System.Diagnostics.Process.Start(topVideo[0].VKPlayer);
                        }
                        else
                            System.Diagnostics.Process.Start("http://costper.ru/wp-content/uploads/2015/07/20244247-1748x984.jpg");
                    }
                }
            }
        }
        public static List<Video> FindTop10Video(List<Video> listVideo)
        {
            List<Video> topVideo = listVideo.OrderByDescending(x => x.Views).Take(10).ToList();
            return topVideo;
        }
        //public static Video FindPopularVideo(List<Video> listVideo) {            
        //    if (listVideo.Count == 0) return null;
        //    Video mostPopularVideo = listVideo.OrderByDescending(x => x.Views).First();
        //    //foreach (var videoCollection in listVideoCollection) {
        //    //    if (!(videoCollection.ListVideo==null))
        //    //    {
        //    //        current = videoCollection.ListVideo.Where(x=>x!=null).OrderByDescending(x => x.Views).First();
        //    //        if (mostPopularVideo==null || mostPopularVideo.Views < current.Views )
        //    //            mostPopularVideo = current;
        //    //    }
        //    //}
        //    return mostPopularVideo;
        //}
        
    }
}

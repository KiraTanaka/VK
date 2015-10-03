using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using xNet.Net;
using xNet.Text;
using System.Windows;

using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace VK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser2.Navigate(String.Format("https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri={2}&display=popup&response_type=token", Program.appID, Program.scope,Program.RedirectUri));
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.Close();
            var urlParams = System.Web.HttpUtility.ParseQueryString(e.Url.Fragment.Substring(1));
            Program.AccessToken = urlParams.Get("access_token");
            Program.UserId = urlParams.Get("user_id");

            string URLFriends = "https://api.vkontakte.ru/method/friends.get?user_id=72813887";
            string URLVideo = "https://api.vk.com/method/video.get?owner_id=72813887&access_token=" + Program.AccessToken;
            //URL="https://vk.com/id72813887?w=wall72813887_2719%2Fall": //url записи на стене
            // sURL = "http://api.vkontakte.ru/method/wall.get?owner_id=92322241&count=100";
            
            WebRequest GetURL = WebRequest.Create(URLFriends);
            Stream StreamGetURL = GetURL.GetResponse().GetResponseStream();
            StreamReader Reader = new StreamReader(StreamGetURL);
            String JsonStringFriends = Reader.ReadToEnd();
            PersonsID person = new PersonsID();
            person.response = JsonConvert.DeserializeObject<PersonsID>(JsonStringFriends).response;         

            WebRequest nGetURL = WebRequest.Create("https://api.vk.com/method/video.get?owner_id=72813887&access_token=" + Program.AccessToken);
            Stream nStreamGetURL = nGetURL.GetResponse().GetResponseStream();
            StreamReader nReader = new StreamReader(nStreamGetURL);
            String JsonStringVideo = nReader.ReadToEnd();
            int IndexBegin = JsonStringVideo.IndexOf('[');
            int IndexEnd = JsonStringVideo.IndexOf(',');
            String CountVideo = JsonStringVideo.Substring(IndexBegin + 1, IndexEnd - IndexBegin - 1);
            JsonStringVideo = JsonStringVideo.Remove(IndexBegin+1,IndexEnd-IndexBegin);
            VideoCollection VideoCollection = new VideoCollection();
            VideoCollection.response = JsonConvert.DeserializeObject<VideoCollection>(JsonStringVideo).response;
            VideoCollection.CountVideo = Int32.Parse(CountVideo);
            int MaxCountViews = 0;
            int IndexVideoMaxCountViews = 0;
            int Index=0;
            foreach(var Video in VideoCollection.response){
                if (Video.views > MaxCountViews)
                {
                    MaxCountViews = Video.views;
                    IndexVideoMaxCountViews=Index;
                }
                Index++;
            }
            //System.Diagnostics.Process.Start("https://vk.com/video" + VideoCollection.response.ElementAt(IndexVideoMaxCountViews).owner_id + "_" + VideoCollection.response.ElementAt(IndexVideoMaxCountViews).vid);
            //https://vk.com/video5144262_170714135   
            System.Diagnostics.Process.Start("https://api.vk.com/method/video.get?owner_id=72813887&access_token=" + Program.AccessToken);
            //GET_http();
        }
    }
}

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
            if (string.IsNullOrWhiteSpace(e.Url.Fragment)) return;
            Control.ParseUrl(e.Url.Fragment.Substring(1));
            Control.Master();
        }
    }
}

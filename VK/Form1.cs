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
using Apache.NMS.Util;
using Apache.NMS;


namespace VK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            reciever();
        }
        private void reciever()
        {
            IConnectionFactory factory = new NMSConnectionFactory("tcp://localhost:61616");
            IConnection connection;
            try
            {
                 connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
            connection.Start();
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            IDestination destination = SessionUtil.GetDestination(session, "Entity");
            IMessageConsumer receiver = session.CreateConsumer(destination);
            receiver.Listener += new MessageListener(Message_Listener);
        }

        private void Message_Listener(IMessage message)
        {
            IObjectMessage objMessage = message as IObjectMessage;
            OperatorRequestObject operatorRequestObject = ((OperatorRequestObject)(objMessage.Body));
            MessageBox.Show(operatorRequestObject.Shortcode);
        }
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    //WebClient client = new WebClient();
        //    //string url="https://oauth.vk.com/authorize?client_id="+Program.appID+"&scope="+Program.scope+"&redirect_uri="+Program.RedirectUri+"&display=popup&response_type=token";
        //    //string jsonStringVideo = client.DownloadString(url);
        //    //webBrowser2.Navigate("https://mail.ru/");
        //    webBrowser2.Navigate(String.Format("https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri={2}&display=popup&response_type=token", Program.appID, Program.scope, Program.RedirectUri));
        //}

        //private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //}

        //private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        //{
        //    this.Close();
        //    //if (string.IsNullOrWhiteSpace(e.Url.Fragment)) return;
        //    //Control.ReceiveUrl();
        //    //Control.Master();

        //}

        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VK
{
    public static class Program
    {
        /// <summary>
        public static int appID = 5088630;
        public static int scope = 4;
        public static string RedirectUri="https://oauth.vk.com/blank.html";
        public static string AccessToken = "";
        public static string UserId = "";
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.ReceiveUrl();
            Control.Master();
            //Application.Run(new Form1());
        }
    }
}

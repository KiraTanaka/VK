using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK;

namespace VKUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            Master master = new Master(new VKDownloadUsers());
            master.Listener();
        }
    }
}

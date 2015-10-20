using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace VK
{
    public class DownloadFriends
    {
        public static PersonsID Load()
        {
            String urlGetFriends = "https://api.vkontakte.ru/method/friends.get?user_id=" + Program.UserId;
            PersonsID persons = new PersonsID();
            WebClient client = new WebClient();
            String jsonStringFriends = client.DownloadString(urlGetFriends);
            if (jsonStringFriends.Contains("error")) return null;
            persons.PersonsId = JsonConvert.DeserializeObject<PersonsID>(jsonStringFriends).PersonsId;
            return persons;
        }
    }
}

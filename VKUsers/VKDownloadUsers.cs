using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VK;

namespace VKUsers
{
    public class VKDownloadUsers : IUserService
    {
        private string UrlVkApi = "https://api.vk.com/method/{0}?{1}";
        private WebClient Client;

        public VKDownloadUsers()
        {
            Client = new WebClient();
            Client.Encoding = System.Text.Encoding.UTF8;
        }
        public string DownloadUserInformation(int uid)
        {
            Person person = new Person();
            var response = Client.DownloadString(String.Format(UrlVkApi, "users.get",String.Format("uids={0}&fields=nickname", uid)));
            if (response.Contains("error")) return null;
            return response;
        }
        public string DownloadId(string mask)
        {
            string jsonStringFriends = Client.DownloadString(String.Format(UrlVkApi,"utils.resolveScreenName",String.Format("screen_name={0}",mask)));
            return jsonStringFriends;
        }
        public string DownloadFriends(int id)
        {
            string jsonStringFriends = Client.DownloadString(String.Format(UrlVkApi, "friends.get", String.Format("user_id={0}&fields=nickname", id)));
            if (jsonStringFriends.Contains("error")) return null;
            return jsonStringFriends;
        }
        public string DownloadMembersOfGroup(int id, int offset)
        {
            string jsonStringMembersOfGroup = Client.DownloadString(String.Format(UrlVkApi, "groups.getMembers", String.Format("group_id={0}&fields=nickname&offset={1}", id, offset)));
            if (jsonStringMembersOfGroup.Contains("error")) return null;
            return jsonStringMembersOfGroup;
        }

    }
}

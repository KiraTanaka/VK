using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VK;
using Newtonsoft.Json;

namespace VKUsers
{
    public class DownloadUsers
    {
        private IUserService service;
        public DownloadUsers(IUserService service) 
        {
            this.service = service;
        }
        public Person GetUserInformation(int uid)
        {
            Person person = new Person();
            var response = service.DownloadUserInformation(uid);
            person = JsonConvert.DeserializeObject<Persons>(response).People[0];
            return person;
        }
        public int GetId(string mask)
        {
            string jsonStringFriends = service.DownloadId(mask);
            ResolveScreenName resolvScreenName = JsonConvert.DeserializeObject<ResponseScreenName>(jsonStringFriends).response;
            return resolvScreenName.ObjectId;
        }
        public List<Person> GetFriends(int id)
        {
            List<Person> persons;
            string jsonStringFriends = service.DownloadFriends(id);
            persons = JsonConvert.DeserializeObject<Persons>(jsonStringFriends).People;
            return persons;
        }
        public List<Person> GetMembersOfGroup(int id)
        {
            int offset = 0;
            List<Person> persons = new List<Person>();
            string jsonStringMembersOfGroup;
            int countPersons = -1;

            while (countPersons != persons.Count)
            {
                jsonStringMembersOfGroup = service.DownloadMembersOfGroup(id, offset);

                if (jsonStringMembersOfGroup.Contains("error")) break;

                if (persons.Count == 0)
                {
                    countPersons = JsonConvert.DeserializeObject<ResponseMembers>(jsonStringMembersOfGroup).response.Count;
                    persons = JsonConvert.DeserializeObject<ResponseMembers>(jsonStringMembersOfGroup).response.People;
                }
                else
                    persons.AddRange(JsonConvert.DeserializeObject<ResponseMembers>(jsonStringMembersOfGroup).response.People);
                offset = persons.Count();
            }
            return persons;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using VK;
using Apache.NMS.Util;
using Apache.NMS;
using Xstream.Core;
using Newtonsoft.Json;

namespace VKUsers
{
    public class Master
    {
        private IConnectionFactory Factory;
        private IConnection Connection;
        private IMessageConsumer Receiver;
        private ISession Session;
        private IDestination Destination;
        private DownloadUsers DownloadUsers;
        public Master(IUserService service)
        {
            DownloadUsers = new DownloadUsers(service);

            Factory = new NMSConnectionFactory("tcp://localhost:61616");           
            Connection = Factory.CreateConnection();
            Connection.Start();
            Session = Connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            Destination = SessionUtil.GetDestination(Session, "Users");
            Receiver = Session.CreateConsumer(Destination);
        }
        public void Listener()
        {
            while (true)
            {
                reciever();
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void reciever()
        {
            Receiver.Listener += new MessageListener(Message_Listener);
        }

        private void Message_Listener(IMessage message)
        {
            ITextMessage textMessage = (ITextMessage) message;
            UserInformationToAdd information = JsonConvert.DeserializeObject<UserInformationToAdd>(textMessage.Text);
            if (information.addMembersOfGroup)
                AddMembersOfGroup(information.url);
            else
                AddUser(information.url,information.addFriends);
        }
        private void AddUser(string url, bool addFriends)
        {
           
                Regex regexUrl = new Regex(@"https://vk.com/(id[0-9]+|[a-z]+)");
                Regex regexId = new Regex(@"id[0-9]+$");
                Regex regexIdWithMask = new Regex(@"m/[a-z]+$");
                List<Person> persons = new List<Person>();
                persons.Add(new Person());

                if (regexUrl.IsMatch(url))
                {
                    if (regexIdWithMask.IsMatch(url))
                        persons[0].UID = DownloadUsers.GetId(regexIdWithMask.Match(url).Value.Remove(0, 2));
                    else
                        persons[0].UID = Int32.Parse(regexId.Match(url).Value.Remove(0, 2));
                    persons[0] = DownloadUsers.GetUserInformation(persons[0].UID);
                    if (addFriends)
                    {
                        persons.AddRange(DownloadUsers.GetFriends(persons[0].UID));
                    }
                    AddingUsersToDatabase(persons);
            }
        }
        private void AddMembersOfGroup(string url)
        {
            Regex regexUrl = new Regex(@"https://vk.com/(public[0-9]+|club[0-9]+|event[0-9]+|[a-z]+)");
            Regex regexIdPublic = new Regex(@"public[0-9]+$");
            Regex regexIdClub = new Regex(@"club[0-9]+$");
            Regex regexIdEvent = new Regex(@"event[0-9]+$");
            Regex regexIdWithMask = new Regex(@"m/[a-z]+$");
            List<Person> persons;
            int id=0;
            if (regexUrl.IsMatch(url))
            {

                if (regexIdWithMask.IsMatch(url))
                    id = DownloadUsers.GetId(regexIdWithMask.Match(url).Value.Remove(0, 2));
                if (regexIdPublic.IsMatch(url))
                    id = Int32.Parse(regexIdPublic.Match(url).Value.Remove(0, 6));
                if (regexIdClub.IsMatch(url))
                    id = Int32.Parse(regexIdClub.Match(url).Value.Remove(0, 4));
                if (regexIdEvent.IsMatch(url))
                    id = Int32.Parse(regexIdEvent.Match(url).Value.Remove(0, 5));
                persons = DownloadUsers.GetMembersOfGroup(id);
                AddingUsersToDatabase(persons);
            }
        }
        private void AddingUsersToDatabase(List<Person> persons)
        {
            using (VKContext db = new VKContext())
            {
                bool flagWrite = false;
                foreach (var person in persons)
                {
                    Person personFromDb = null;
                    personFromDb = db.People.FirstOrDefault(x => x.UID == person.UID);
                    if (personFromDb == null)
                    {
                        db.People.Add(DownloadUsers.GetUserInformation(person.UID));
                        flagWrite = true;
                    }
                }
                if (flagWrite)
                    db.SaveChanges();
            }
        }
    }
}

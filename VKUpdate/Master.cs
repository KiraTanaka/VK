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
        public IConnectionFactory factory;
        public IConnection Connection;
        public IMessageConsumer Receiver;
        public ISession Session;
        public IDestination destination;
        public Master()
        {
            factory = new NMSConnectionFactory("tcp://localhost:61616");           
            Connection = factory.CreateConnection();
            Connection.Start();
            Session = Connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            destination = SessionUtil.GetDestination(Session, "Users");
            Receiver = Session.CreateConsumer(destination);
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
            AddUser(information.userUrl,information.addFriends);
        }
        private void AddUser(string userUrl, bool addFriends)
        {
            using (VKContext db = new VKContext())
            {
                bool flagWrite = false;
                Regex regexUrl = new Regex(@"https://vk.com/(id[0-9]+|[a-z]+)");
                Regex regexId = new Regex(@"id[0-9]+$");
                Regex regexIdWithMask = new Regex(@"m/[a-z]+$");
                List<Person> persons = new List<Person>();
                persons.Add(new Person());

                if (regexUrl.IsMatch(userUrl))
                {
                    if (regexIdWithMask.IsMatch(userUrl))
                    {
                        string mask = regexIdWithMask.Match(userUrl).Value.Remove(0, 2);
                        persons[0].UID = DownloadUsers.GetUserId(mask);
                    }
                    else
                        persons[0].UID = Int32.Parse(regexId.Match(userUrl).Value.Remove(0, 2));
                    persons[0] = DownloadUsers.DownloadUserInformation(persons[0].UID);
                    if (addFriends)
                    {
                        persons.AddRange(DownloadUsers.DownloadFriends(persons[0].UID));
                    }                   
                    foreach (var person in persons)
                    {
                        Person personFromDb = null;
                        personFromDb = db.People.FirstOrDefault(x => x.UID == person.UID);
                        if (personFromDb == null)
                        {
                            db.People.Add(DownloadUsers.DownloadUserInformation(person.UID));
                            flagWrite = true;
                        }
                    }
                    if (flagWrite)
                        db.SaveChanges();
                }
            }
        }
    }
}

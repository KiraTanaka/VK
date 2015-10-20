using System;
using NUnit.Framework;
using VK;

namespace VK.UnitTests
{
    [TestFixture]
    public class DownloadFriendsTests
    {
        [Test]
        public void Load_FindFriends_ReturnCorrectPersons()
        {
            Program.UserId = "72813887";
            Assert.That(DownloadFriends.Load(),Is.TypeOf<PersonsID>().Or.Null);
            PersonsID persons = DownloadFriends.Load();
            foreach (var personId in persons.PersonsId) {
                Assert.That(personId, Is.Not.Negative);
            }          
        }
        
    }
}

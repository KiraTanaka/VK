using System;
using NUnit.Framework;
using VK;
using System.Text.RegularExpressions;
using System.Collections.Generic;

//-----------------------------------
//Методика именования методов, тестов, проекта взята из книги The Art Of Unit testing
//<Тестируемый проект>.UnitTests
//<Тестируемый класс>Tests
//Именование методов: 
//    ЧтоТестируем_СостояниеТеста_ОжидаемоеПоведение 
//    (в оригинале UnitOfWork_StateUnderTest_ExpectedBehavior)

//В правильности названия методов сомневаюсь.
//-----------------------------------


namespace VK.UnitTests
{
    [TestFixture]
    public class ControlTests
    {
        [Test]
        public void FindPopularVideo_CheckIndex_ReturnCorrectInt()
        {
            Program.AccessToken = "e8b30b3f7a24f2357ec959c417b3b2326561c463e35d87143ea4b02c5a248d4f93276ce145dc2d7395e04";
            int UserId=72813887;
            int indexVideoMaxCountViews = Control.FindPopularVideo(DownloadVideo.Load(UserId));
            Assert.That(indexVideoMaxCountViews, Is.Not.Null.And.Not.Negative);

        }
        [Test]
        public void FindPopularVideoFriends_CheckOwneridAndId_ReturnCorrectString()
        {
            Program.AccessToken = "e8b30b3f7a24f2357ec959c417b3b2326561c463e35d87143ea4b02c5a248d4f93276ce145dc2d7395e04";
            Program.UserId = "72813887";
            var pattern = @"[-]?[0-9]+_[0-9]+";//-82310056_171325373
            string VideoMaxCountViews = Control.FindPopularVideoFriends(DownloadFriends.Load().PersonsId);
            Assert.That(VideoMaxCountViews, Is.Not.Null);
            Assert.That(Regex.IsMatch(VideoMaxCountViews, pattern));
        }
    }
}
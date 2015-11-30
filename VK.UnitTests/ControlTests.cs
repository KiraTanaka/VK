using System;
using NUnit.Framework;
using System.Linq;
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
        List<Video> FillingListVideo(List<Video> listVideo)
        {
            int[] arrayValue = new int[] {9787, 19779, 4636 , 2345, 55674, 357    , 23598, 546746, 75 ,
                                          9487, 43626, 57  , 3456, 74677, 46   ,243626,234624,  35 ,
                                          5745, 34577, 7 , 4556, 35738, 6 ,3345,34567,     5, 
                                          9458, 34566, 1};
            for (int i = 0; i < 3; i++)
            {
                listVideo.Add(new Video() { Vid = arrayValue[i + i * 8], OwnerId = arrayValue[i + 1 + i * 8], Views = arrayValue[i + 2 + i * 8] });
                listVideo.Add(new Video() { Vid = arrayValue[i + 3 + i * 8], OwnerId = arrayValue[i + 4 + i * 8], Views = arrayValue[i + 5 + i * 8] });
                listVideo.Add(new Video() { Vid = arrayValue[i + 6 + i * 8], OwnerId = arrayValue[i + 7 + i * 8], Views = arrayValue[i + 8 + i * 8] });
            }
            listVideo.Add(new Video() { Vid = arrayValue[27], OwnerId = arrayValue[28], Views = arrayValue[29] });
            return listVideo;
        }
        [Test]
        public void FindTop10VideoFriends_ListVideoIsNull()
        {
            List<Video> listVideo = new List<Video>();
            Assert.AreEqual(null, Control.FindTop10Video(listVideo));
        }
        [Test]
        public void FindTop10VideoFriends_ValidationWork()
        {
            List<Video> listVideo= new List<Video>();
            listVideo = FillingListVideo(listVideo);
            Assert.AreEqual(listVideo, Control.FindTop10Video(listVideo));
        }
        [Test]
        public void FindTop10VideoFriends_VideoIsNull()
        {
            List<Video> listVideo = new List<Video>();
            listVideo = FillingListVideo(listVideo);
            listVideo.Add(null);
            listVideo.Add(new Video { Vid = 82474, OwnerId = 924898, Views = 987654 });
            List<Video> listVideoResult = new List<Video>();
            listVideoResult.Add(listVideo[11]);
            listVideoResult.AddRange(listVideo.Take(9).ToList());
            Assert.AreEqual(listVideoResult, Control.FindTop10Video(listVideo));
        }
        [Test]
        public void FindTop10VideoFriends_CountOfListVideoLess10()
        {
            List<Video> listVideo = new List<Video>();
            listVideo = FillingListVideo(listVideo);
            listVideo.RemoveAt(9);
            Assert.AreEqual(listVideo, Control.FindTop10Video(listVideo));
        }
        [Test]
        public void FindTop10VideoFriends_EmptyFieldVideo()
        {
            List<Video> listVideo = new List<Video>();
            listVideo = FillingListVideo(listVideo);
            listVideo.Add(new Video { Vid = 0, OwnerId = 0, Views = 0 });
            List<Video> listVideoResult = new List<Video>();
            listVideoResult = listVideo.Take(10).ToList();
            Assert.AreEqual(listVideoResult, Control.FindTop10Video(listVideo));
        }

    }
}
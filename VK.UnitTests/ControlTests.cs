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
        List<VideoCollection> FillingListVideoCollection(List<VideoCollection> listVideoCollection)
        {
            int[] arrayValue = new int[] {9787, 19779, 78 , 2345, 55674, 5    , 23598, 546746, 57 ,//VideoCollection{Video,Video,Video}
                                          9487, 43626, 7  , 3456, 74677, 35   ,243626,234624,  6 ,
                                          5745, 34577, 46 , 4556, 35738, 4636 ,3345,34567,     357 };
            for (int i = 0; i < 3; i++)
            {
                VideoCollection videoCollection = new VideoCollection();
                videoCollection.ListVideo = new List<Video>();
                videoCollection.ListVideo.Add(new Video() { Vid = arrayValue[i + i * 8], OwnerId = arrayValue[i + 1 + i * 8], Views = arrayValue[i + 2 + i * 8] });
                videoCollection.ListVideo.Add(new Video() { Vid = arrayValue[i + 3 + i * 8], OwnerId = arrayValue[i + 4 + i * 8], Views = arrayValue[i + 5 + i * 8] });
                videoCollection.ListVideo.Add(new Video() { Vid = arrayValue[i + 6 + i * 8], OwnerId = arrayValue[i + 7 + i * 8], Views = arrayValue[i + 8 + i * 8] });
                listVideoCollection.Add(videoCollection);

            }
            return listVideoCollection;
        }
        [Test]
        public void FindPopularVideoFriends_ListVideoCollectionIsNull()
        {
            List<VideoCollection> listVideoCollection = new List<VideoCollection>();
            Assert.AreEqual(null, Control.FindPopularVideo(listVideoCollection));
        }
        [Test]
        public void FindPopularVideoFriends_ValidationWork()
        {
            List<VideoCollection> listVideoCollection = new List<VideoCollection>();
            listVideoCollection = FillingListVideoCollection(listVideoCollection);
            Video popularVideo = listVideoCollection[2].ListVideo[1]; 
            Assert.AreEqual(popularVideo,Control.FindPopularVideo(listVideoCollection));
        }
        [Test]
        public void FindPopularVideoFriends_VideoIsNull()
        {
            List<VideoCollection> listVideoCollection = new List<VideoCollection>();
            listVideoCollection = FillingListVideoCollection(listVideoCollection);
            listVideoCollection[0].ListVideo.Add(null);
            listVideoCollection[0].ListVideo.Add(new Video { Vid = 82474, OwnerId = 924898, Views = 987654 });
            Video popularVideo = listVideoCollection[0].ListVideo[4];
            Assert.AreEqual(popularVideo, Control.FindPopularVideo(listVideoCollection));
        }
        [Test]
        public void FindPopularVideoFriends_EmptyFieldVideo()
        {
            List<VideoCollection> listVideoCollection = new List<VideoCollection>();
            listVideoCollection = FillingListVideoCollection(listVideoCollection);
            listVideoCollection[0].ListVideo.Add(new Video { Vid = 0, OwnerId = 0, Views = 0 });
            Video popularVideo = listVideoCollection[2].ListVideo[1];
            Assert.AreEqual(popularVideo, Control.FindPopularVideo(listVideoCollection));
        }

    }
}
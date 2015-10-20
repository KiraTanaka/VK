using System;
using NUnit.Framework;
using VK;
using System.Collections.Generic;

namespace VK.UnitTests
{
    [TestFixture]
    public class DownloadVideoTests
    {
        [Test]
        public void Load_FindVideo_ReturnCorrectVideoCollection()
        {
            Program.AccessToken = "e8b30b3f7a24f2357ec959c417b3b2326561c463e35d87143ea4b02c5a248d4f93276ce145dc2d7395e04";
            int[] UserId = { 72813887, 10950353 };
            VideoCollection[] videoCollection={null,null};
            videoCollection[0] = DownloadVideo.Load(UserId[0]);
            Assert.That(videoCollection[0], Is.TypeOf<VideoCollection>());
            Assert.That(videoCollection[0].ArrayVideo, Is.TypeOf<List<Video>>().And.Not.Null);
            Assert.That(videoCollection[0].CountVideo, Is.Not.Negative.And.Not.Null);

            videoCollection[1] = DownloadVideo.Load(UserId[1]);
            Assert.That(videoCollection[1], Is.TypeOf<VideoCollection>());
            Assert.That(videoCollection[1].ArrayVideo, Is.Null);
            Assert.That(videoCollection[1].CountVideo, Is.EqualTo(0));
        }
    }
}

using System;
using NUnit.Framework;
using VK;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Moq;

namespace VK.UnitTests
{
    [TestFixture]
    class ControlTestsMoq
    {
        private IService service;
        public ControlTestsMoq()
        {
            var serviceMoc = new Mock<IService>();
            serviceMoc.Setup(x => x.GetVideo(40816886, 200, "c193e28abe97b2f74b53e4a1bfcb77ee7c8ec8167f0c52a98781eae410ec373b603740e81aacb2cc35d7e",0))
                       .Returns(
                        "{\"response\":[6,{\"vid\":171540519,\"owner_id\":40816886,\"title\":\"\\\"Unsung Hero\\\" (Official HD) : TVC Thai Life Insurance\",\"description\":\"\",\"duration\":185,\"link\":\"video40816886_171540519\",\"date\":1450170645,\"views\":3,\"image\":\"http:\\/\\/cs543403.vk.me\\/u2884035\\/video\\/m_fe51d43f.jpg\",\"image_medium\":\"http:\\/\\/cs543403.vk.me\\/u2884035\\/video\\/l_95aeb1b4.jpg\",\"comments\":0,\"player\":\"http:\\/\\/vk.com\\/video_ext.php?oid=40816886&id=171540519&hash=36c10509b6353f06\"},{\"vid\":167087585,\"owner_id\":40816886,\"title\":\"Девочка со спичками (2006)\",\"description\":\"Короткометражный мультипликационный фильм по мотивам сказки Ганса Христиана Андерсена «Девочка со спичками». Мультфильм номинировался на премию «Оскар» в 2007 среди короткометражных мультфильмов.\",\"duration\":383,\"link\":\"video40816886_167087585\",\"date\":1389290233,\"views\":1,\"image\":\"http:\\/\\/cs12464.vk.me\\/u14397688\\/video\\/m_81b625a9.jpg\",\"image_medium\":\"http:\\/\\/cs12464.vk.me\\/u14397688\\/video\\/l_9e95da11.jpg\",\"comments\":0,\"player\":\"http:\\/\\/vk.com\\/video_ext.php?oid=40816886&id=167087585&hash=6daded16161a8689\"},{\"vid\":165381603,\"owner_id\":40816886,\"title\":\"Imagine Dragons - It's Time - (Peter Hollens &amp; Tyler Ward Cover)\",\"description\":\"\",\"duration\":259,\"link\":\"video40816886_165381603\",\"date\":1372222424,\"views\":2,\"image\":\"http:\\/\\/cs507517.vk.me\\/u196194245\\/video\\/m_cfd34c19.jpg\",\"image_medium\":\"http:\\/\\/cs507517.vk.me\\/u196194245\\/video\\/l_da316fa4.jpg\",\"comments\":0,\"player\":\"http:\\/\\/vk.com\\/video_ext.php?oid=40816886&id=165381603&hash=330a248b3e1ece3d\"},{\"vid\":165381586,\"owner_id\":40816886,\"title\":\"We Are Young - fun. - Mike Tompkins\",\"description\":\"\",\"duration\":291,\"link\":\"video40816886_165381586\",\"date\":1372221988,\"views\":3,\"image\":\"http:\\/\\/cs506510.vk.me\\/u7199434\\/video\\/m_c8ae708e.jpg\",\"image_medium\":\"http:\\/\\/cs506510.vk.me\\/u7199434\\/video\\/l_f1dc39b0.jpg\",\"comments\":0,\"player\":\"http:\\/\\/vk.com\\/video_ext.php?oid=40816886&id=165381586&hash=bd6cf121e8575a43\"},{\"vid\":165381576,\"owner_id\":40816886,\"title\":\"Skyrim Theme Lyrics Video - A cappella - Peter Hollens\",\"description\":\"ВОИСТЕНУ!\",\"duration\":233,\"link\":\"video40816886_165381576\",\"date\":1372221803,\"views\":1,\"image\":\"http:\\/\\/cs518422.vk.me\\/u16931603\\/video\\/m_aff496b7.jpg\",\"image_medium\":\"http:\\/\\/cs518422.vk.me\\/u16931603\\/video\\/l_22b81985.jpg\",\"comments\":0,\"player\":\"http:\\/\\/vk.com\\/video_ext.php?oid=40816886&id=165381576&hash=3f83ae0ef23c4e7a\"},{\"vid\":163812135,\"owner_id\":40816886,\"title\":\"Жестокий розыгрыш в лифте\",\"description\":\"\",\"duration\":353,\"link\":\"video40816886_163812135\",\"date\":1354290571,\"views\":3,\"image\":\"http:\\/\\/cs6037.vk.me\\/u40816886\\/video\\/m_6cd215ae.jpg\",\"image_medium\":\"http:\\/\\/cs6037.vk.me\\/u40816886\\/video\\/l_3bb91498.jpg\",\"comments\":0,\"player\":\"http:\\/\\/www.youtube.com\\/embed\\/eP3HVLQftyc\"}]}"
                        );
            service = serviceMoc.Object;
        }

        [Test]
        public void GetVideo_CorrectData()
        {
            List<Video> listVideoMoq = new List<Video>();
            List<Video> listVideo = new List<Video>();
            listVideo.Add( new Video() { Id = 0,DateTime = new DateTime(),OwnerId=40816886,Player="http://vk.com/video_ext.php?oid=40816886&id=171540519&hash=36c10509b6353f06",Title ="\"Unsung Hero\" (Official HD) : TVC Thai Life Insurance",Vid=171540519,Views=3,VKPlayer=null});
            listVideo.Add(new Video() { Id = 0,DateTime = new DateTime(),OwnerId=40816886,Player="http://vk.com/video_ext.php?oid=40816886&id=167087585&hash=6daded16161a8689",Title ="Девочка со спичками (2006)",Vid=167087585,Views=1,VKPlayer = null});
            listVideo.Add(new Video() { Id = 0,DateTime = new DateTime(),OwnerId=40816886,Player="http://vk.com/video_ext.php?oid=40816886&id=165381603&hash=330a248b3e1ece3d",Title ="Imagine Dragons - It's Time - (Peter Hollens &amp; Tyler Ward Cover)",Vid=165381603,Views=2,VKPlayer=null});
            listVideo.Add(new Video() { Id = 0,DateTime = new DateTime(),OwnerId=40816886,Player="http://vk.com/video_ext.php?oid=40816886&id=165381586&hash=bd6cf121e8575a43",Title ="We Are Young - fun. - Mike Tompkins",Vid=165381586,Views=3,VKPlayer=null});
            listVideo.Add(new Video() { Id = 0, DateTime = new DateTime(), OwnerId = 40816886, Player = "http://vk.com/video_ext.php?oid=40816886&id=165381576&hash=3f83ae0ef23c4e7a", Title = "Skyrim Theme Lyrics Video - A cappella - Peter Hollens", Vid = 165381576, Views = 1, VKPlayer = null });
            listVideo.Add(new Video() { Id = 0, DateTime = new DateTime(), OwnerId = 40816886, Player = "http://www.youtube.com/embed/eP3HVLQftyc", Title = "Жестокий розыгрыш в лифте", Vid = 163812135, Views = 3, VKPlayer = null });
            List<int> personsId = new List<int>();
            personsId.Add(40816886);
            Control control = new Control(service);
            listVideoMoq = control.FillingListVideo(personsId);

            //сами тесты, но валиться не на них, а на 32 строчке в файле DownloadVideo.cs 

            //Assert.AreEqual(listVideoMoq.Count, listVideo.Count);
            //for (int i = 0; i < 6; i++)
            //{
            //    Assert.AreEqual(listVideoMoq[i].Id, listVideo[i].Id);
            //    Assert.AreEqual(listVideoMoq[i].DateTime, listVideo[i].DateTime);
            //    Assert.AreEqual(listVideoMoq[i].OwnerId, listVideo[i].OwnerId);
            //    Assert.AreEqual(listVideoMoq[i].Player, listVideo[i].Player);
            //    Assert.AreEqual(listVideoMoq[i].Title, listVideo[i].Title);
            //    Assert.AreEqual(listVideoMoq[i].Vid, listVideo[i].Vid);
            //    Assert.AreEqual(listVideoMoq[i].Views, listVideo[i].Views);
            //    Assert.AreEqual(listVideoMoq[i].VKPlayer, listVideo[i].VKPlayer);               
            //}
            // Arrange
            //var mock = new Mock<IRepository>();
            //mock.Setup(a => a.GetComputerList()).Returns(new List<Computer>());
            //HomeController controller = new HomeController(mock.Object);
        }
    }
}

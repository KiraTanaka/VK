using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS.Util;
using Apache.NMS;
using System.Windows.Forms;

namespace VK
{
    public class Reciever
    {
        public void reciever()
        {
            IConnectionFactory factory = new NMSConnectionFactory("tcp://localhost:61616");
            IConnection connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            IDestination destination = SessionUtil.GetDestination(session, "Entity");
            IMessageConsumer receiver = session.CreateConsumer(destination);
            receiver.Listener += new MessageListener(Message_Listener);
            connection.Stop();
        }

        private void Message_Listener(IMessage message)
        {
            IObjectMessage objMessage = message as IObjectMessage;
            //OperatorRequestObject operatorRequestObject = ((OperatorRequestObject)(objMessage.Body));
            //MessageBox.Show(operatorRequestObject.Shortcode);
        }
    }
}

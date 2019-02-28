using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewingDataModel.Util
{
    class Message
    {
        string sender;
        string recipient;
        string messageText;

        public Message(string sender, string recipient, string messageText)
        {
            this.sender = sender;
            this.recipient = recipient;
            this.messageText = messageText;
        }

        public void Send()
        {

        }
    }
}

using jmessage.message;
using System.Collections.Generic;

namespace example.MessageExamples
{
    class SendTextMessageExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            MessageClient client = new MessageClient(app_key, master_secret);
            TextMessageBody msg_body = new TextMessageBody();
            msg_body.text = "Hello world";

            Dictionary<string, string> extras = new Dictionary<string, string>();
            extras.Add("name", "lhw");
            msg_body.extras = extras;

            TextMessagePayload payload = new TextMessagePayload("1", "single", "admin", "custom", "lhw1", "test02", msg_body);
            MessagePayload msgPayload = new MessagePayload();
            client.sendMessage(payload);
        }
    }
}

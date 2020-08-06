using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<ServerUser> users = new List<ServerUser>();
        private int nextId = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser
            {
                Id = nextId,
                Name = name,
                OperationContext = OperationContext.Current
            };
            nextId++;
            users.Add(user);

            SendMessage($"{user.Name} подключился к чату", 0);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.Find(x => x.Id == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.Name} отключился от чата", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var user in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var sender = users.Find(x => x.Id == id);
                if (sender != null)
                    answer += ": " + user.Name + " ";

                answer += message;

                user.OperationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }

        }
    }
}

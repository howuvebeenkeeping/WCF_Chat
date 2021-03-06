﻿using System;
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
            ServerUser user = new ServerUser()
            {
                Id = nextId,
                Name = name,
                OperationContext = OperationContext.Current
            };
            nextId++;

            SendMessage(" " + user.Name + " has connected.", 0);

            users.Add(user);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.Id == id);

            if (user != null)
            {
                users.Remove(user);
                SendMessage(" " + user.Name + " has left.", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in users)
            {
                string answer = $"[{DateTime.Now.ToShortTimeString()}]";

                var user = users.FirstOrDefault(i => i.Id == id);

                if (user != null)
                    answer += " " + user.Name + ": ";

                answer += message;
                item.OperationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatServiceLibrary
{
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat

    {
        [OperationContract]
        void Connect();

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }

    public interface IServerChatCallback
    {
        [OperationContract]
        void MessageCallback(string message);
    }
}

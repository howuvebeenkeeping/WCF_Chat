using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Chat
{
    public class ServerUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public OperationContext OperationContext { get; set; }
    }
}

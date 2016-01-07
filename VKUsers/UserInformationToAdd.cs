using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKUsers
{
    [Serializable]
    public class UserInformationToAdd
    {
        public string url;
        public bool addFriends;
        public bool addMembersOfGroup;
    }
}

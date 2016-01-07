using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKUsers
{
    public interface IUserService
    {
        string DownloadUserInformation(int uid);
        string DownloadId(string mask);
        string DownloadFriends(int id);
        string DownloadMembersOfGroup(int id, int offset);
    }
}

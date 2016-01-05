using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK
{
    public interface IService
    {
        string GetVideo(int ownerId, int count, string access_token, int offset = 0);
    }
}

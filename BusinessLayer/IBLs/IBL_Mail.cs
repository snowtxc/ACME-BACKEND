using DataAccessLayer.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public interface IBL_Mail
    {
        public void sendMail(string receptor, string asunto, string content);
    }
}

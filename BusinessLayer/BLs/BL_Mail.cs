using DataAccessLayer.IDALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Mail: IBL_Mail
    {
        private IDAL_Mail _mail;

        public BL_Mail(IDAL_Mail mail)
        {
            _mail = mail;
        }

        public void sendMail(string receptor, string asunto, string content)
        {
            _mail.sendMail(receptor, asunto, content);
        }
    }
}

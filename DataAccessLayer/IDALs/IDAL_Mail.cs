
namespace DataAccessLayer.IDALs
{
    public interface IDAL_Mail
    {
        void sendMail(string receptor, string asunto, string content);
    }
}

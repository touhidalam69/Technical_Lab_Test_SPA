using System.Configuration;
using System.Data.SqlClient;

namespace Operational.DAL
{
    public class Connection
    {
        public static SqlConnection GetConnection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["TouhidString"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr); con.Open(); return con;
        }
    }
}

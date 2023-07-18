using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace DataManagement.Pages.Datamanager
{
    
    public class Index1Model : PageModel
    {
        private readonly ILogger<Index1Model> _logger;

        public Index1Model(ILogger<Index1Model> logger)
        {
            _logger = logger;
        }

        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString =
                    "Data Source=PUNITHKUMAR\\SQLEXPRESS;Initial Catalog=DataStoreEx;Persist Security Info=True;User ID=sa;Password=pass@word!";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String Sql = "Select * from Client";
                    using (SqlCommand scom = new SqlCommand(Sql, conn))
                    {
                        using (SqlDataReader reader = scom.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo cli = new ClientInfo();
                                cli.id = "" + reader.GetInt32(0);
                                cli.name = reader.GetString(1);
                                cli.email = reader.GetString(2);
                                cli.phone = reader.GetString(3);
                                cli.address = reader.GetString(4);
                                cli.created_at = reader.GetDateTime(5).ToString();
                                listClients.Add(cli);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception is : " + e.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}

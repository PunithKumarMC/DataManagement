using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataManagement.Pages.Datamanager
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public ClientInfo clientInfo = new ClientInfo();
        public String errmsg = "";
        public String sucmsg = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errmsg = "All fields are Required";
                return;
            }

            //save into database

            try
            {
                String Strconnection =
                    "Data Source=PUNITHKUMAR\\SQLEXPRESS;Initial Catalog=DataStoreEx;Persist Security Info=True;User ID=sa;Password=pass@word!";
                using(SqlConnection connection = new SqlConnection(Strconnection))
                {
                    connection.Open();
                    String sql =
                        "Insert Into Client(name, email,phone, address) values (@name,@email,@phone, @address);";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);
                        cmd.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return;

            }





            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            sucmsg = "New data added successfully";

            Response.Redirect("/Datamanager/Index1");
        }
    }
}

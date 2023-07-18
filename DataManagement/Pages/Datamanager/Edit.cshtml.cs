using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace DataManagement.Pages.Datamanager
{
    public class Edit : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errmsg = "";
        public String sucmsg = "";
        public void OnGet()
        {
             String id = Request.Query["id"];
            try
            {
                String Strconnection =
                    "Data Source=PUNITHKUMAR\\SQLEXPRESS;Initial Catalog=DataStoreEx;Persist Security Info=True;User ID=sa;Password=pass@word!";
                using (SqlConnection connection = new SqlConnection(Strconnection))
                {
                    connection.Open();
                    String sql =
                        "Select * from Client where id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return;

            }

        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
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
                using (SqlConnection connection = new SqlConnection(Strconnection))
                {
                    connection.Open();
                    String sql =
                        "update Client set name=@name, email=@email, phone =@phone, address = @address where id = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", clientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", clientInfo.address);
                        cmd.Parameters.AddWithValue("@id", clientInfo.id);
                        cmd.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception e)
            {
                errmsg = e.Message;
                return;

            }

            Response.Redirect("/Datamanager/Index1");
        }
    }
}

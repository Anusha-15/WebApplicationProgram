using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplicationProgram.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void onPost()
        {
            clientInfo.Name= Request.Form["name"];
            clientInfo.email= Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address= Request.Form["address"];

            if(clientInfo.Name.Length==0||clientInfo.email.Length==0||clientInfo
                .phone.Length==0||clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }

            //save the new client into the database

            try
            {
                string connectionString = "Data Source=DESKTOP-FEUG4AJ\\SQLEXPRESS;Initial Catalog=userform;" +
                    "Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients" +
                        "(Name, email, phone, address) VALUES" +
                        "(@Name, @email,@phone,@address);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@Name", clientInfo.Name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();}
                }
            }
            catch (Exception ex) {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.Name = ""; clientInfo.email = "";clientInfo.phone = "";clientInfo.address = "";
            successMessage = "New Client added correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}

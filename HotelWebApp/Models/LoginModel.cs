//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Data.SqlClient;

namespace HotelWebApp.Models
{
    public interface ILoginService
    {
         List<Claim> LogMeIn(LoginModel objLoginModel);
        void LogMeOut();
    }
    public class LoginService:ILoginService
    {
        private IConfiguration _configuration;
        public LoginService(IConfiguration config)
        {
            _configuration = config;
        }
        public List<Claim> LogMeIn(LoginModel objLoginModel)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            List<Claim> claims = null;

            try
            {
                string SqlCon = this._configuration.GetConnectionString("HotelManagementDBConnectionString");
                con = new SqlConnection(SqlCon);
                string SqlQuery = "Select Registration_ID,UserID,Password,Usertype from Registrations " +
                    "Where UserID=@UserId and Password=@Password";

                con.Open();
                cmd = new SqlCommand(SqlQuery, con);
                cmd.Parameters.AddWithValue("@UserId", objLoginModel.UserName);
                cmd.Parameters.AddWithValue("@Password", objLoginModel.Password);
                SqlDataReader sdr = cmd.ExecuteReader();

                if(sdr.Read())
                {
                    //A claim is stmt about a subject by an issuer and represent attributes of sub
                    //that are usefull in context of authentication and authorization oprations.
                    claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,Convert.ToString(sdr["Registration_ID"])),
                        new Claim(ClaimTypes.Name,objLoginModel.UserName),
                        new Claim(ClaimTypes.Role,sdr["Usertype"].ToString()),
                        new Claim("FavoriteDrink","Tea")
                    };
                    objLoginModel.Role = sdr["Usertype"].ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return claims;
        }
        public void LogMeOut()
        {

        }
    }
    //storing the data for user login
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
        public string Role { get; set; }
    }
    //storing data for all user in usermodel
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

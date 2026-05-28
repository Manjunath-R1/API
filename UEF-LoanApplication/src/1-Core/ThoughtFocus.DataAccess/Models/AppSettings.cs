namespace ThoughtFocus.DataAccess.Models
{
    public class AppSettings
    {
        public string JWTSecretKey { get; set; }
        public string PasswordSalt { get; set; }
        public int JWTTokenExpirydate { get; set; }
        public string BaseUrl { get; set; }        
        public string GeneralEmail { get; set; }
        public string Api2PDFKey { get; set; }        

    }

}
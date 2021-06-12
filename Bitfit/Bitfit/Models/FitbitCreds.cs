namespace Bitfit.Models
{
    public class FitbitCreds
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string UserId { get; set; }
    }
}

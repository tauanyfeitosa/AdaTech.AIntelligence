namespace AdaTech.AIntelligence.Service.DTOs.ModelRequest
{
    public class DTOUserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public DTOUserToken(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}

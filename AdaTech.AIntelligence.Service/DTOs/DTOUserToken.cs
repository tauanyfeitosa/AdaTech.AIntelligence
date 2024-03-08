namespace AdaTech.AIntelligence.Service.DTOs
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

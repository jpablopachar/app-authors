namespace app_authors.Dtos
{
    public class AuthenticationResponseDto
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
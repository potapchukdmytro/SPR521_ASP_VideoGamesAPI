namespace SPR521_VideoGames.DAL.Entities
{
    public enum TokenType
    {
        ConfirmEmail,
        ChangeEmail
    };

    public class UserToken : BaseEntity
    {
        public required string Token { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddMinutes(15);
        public string? NewEmail { get; set; }
        public TokenType Type { get; set; } = TokenType.ConfirmEmail;

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}

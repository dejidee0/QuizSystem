namespace QuizAppSystem.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JwtToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateRegistered { get; set; }
        public string ConfirmationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public string EmailConfirmationToken { get; set; }
        public DateTime? PasswordResetExpiry { get; set; }
    }
}
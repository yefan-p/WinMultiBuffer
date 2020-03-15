namespace MultiBuffer.IWebApi
{
    /// <summary>
    /// Информация для аутентификации пользователя
    /// </summary>
    public class AuthenticateUser
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}

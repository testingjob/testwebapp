namespace TestWebApp.BLL
{
    public class PasswordManager
    {
        private readonly HashComputer _hashComputer = new HashComputer();

        public string GeneratePasswordHash(string plainTextPassword, out string salt) {
            salt = SaltGenerator.GetSaltString();
            var finalString = plainTextPassword + salt;

            return _hashComputer.GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash) {
            var finalString = password + salt;
            return hash == _hashComputer.GetPasswordHashAndSalt(finalString);
        }
    }
}
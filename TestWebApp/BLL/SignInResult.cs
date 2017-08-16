using System.Collections.Generic;
using System.Security.Authentication;

namespace TestWebApp.BLL
{
    public class SignInResult
    {
        public bool Success => Exceptions.Count == 0;
        public List<AuthenticationException> Exceptions { get; set; }

        public SignInResult()
        {
            Exceptions = new List<AuthenticationException>();
        }
    }
}
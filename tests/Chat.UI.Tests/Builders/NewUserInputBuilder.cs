using Chat.UI.Models;

namespace Chat.UI.Tests.Builders
{
    public class NewUserInputBuilder
    {
        private string _Username { get; set; }
        private string _Password { get; set; }
        private string _PasswordConfirmation { get; set; }
        private string _Email { get; set; }

        internal NewUserInputBuilder()
        {
            _Username = "user";
            _Password = "P@ssword1";
            _PasswordConfirmation = "P@ssword1";
            _Email = "user@user.com";
        }
       
        internal RegisterViewModel Build()
        {
            return new RegisterViewModel
            {
                UserName = _Username,
                Password = _Password,
                ConfirmPassword = _PasswordConfirmation,
                Email = _Email,
            };
        }
    }
}

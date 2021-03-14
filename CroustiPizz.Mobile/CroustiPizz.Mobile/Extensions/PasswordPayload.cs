namespace CroustiPizz.Mobile.Extensions
{
    public class PasswordPayload
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public PasswordPayload(string currentPassword, string newPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}
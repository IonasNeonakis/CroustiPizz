namespace CroustiPizz.Mobile.Extensions
{
    /// <summary>
    /// Cette classe encapsule les informations nécessaires lors d'un changement de mot de passe :
    /// - le mot de passe actuel
    /// - le nouveau mot de passe voulu
    /// </summary>
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
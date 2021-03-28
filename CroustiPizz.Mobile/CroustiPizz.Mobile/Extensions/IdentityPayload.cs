namespace CroustiPizz.Mobile.Extensions
{
    /// <summary>
    /// Cette classe encapsule les informations nécessaires à un changement d'identité depuis le profil :
    /// - le prénom actuel
    /// - le nom de famille actuel
    /// </summary>
    public class IdentityPayload
    {
        public string CurrentFirstName { get; set; }
        public string CurrentLastName { get; set; }

        public IdentityPayload(string currentFirstName, string currentLastName)
        {
            CurrentFirstName = currentFirstName;
            CurrentLastName = currentLastName;
        }
    }
}
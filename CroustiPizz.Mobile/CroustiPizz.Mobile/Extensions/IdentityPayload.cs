namespace CroustiPizz.Mobile.Extensions
{
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
namespace WSSendMessage.Settings
{
    public class SendGridConfig
    {
            public string? AccountSid { get; set; }
            public string? AuthToken { get; set; }
            public string? MessagingServiceSid { get; set; }

            public override string ToString()
            {
                return String.Concat(AccountSid, " || ", AuthToken, " || ", MessagingServiceSid);
            }
    }
}

namespace FribergHomeClient.Data
{
    //Author:Emelie
    public static class StatusTypes
    {
        public enum StatusType
        {
            Pending,
            Approved,
            Denied
        }

        public static string GetLocalized(StatusType type)
        {
            return type switch
            {
                StatusType.Pending => "Inväntar svar",
                StatusType.Approved => "Godkänd",
                StatusType.Denied => "Nekad",
                _ => "Ej vald",
            };
        }
    }
}


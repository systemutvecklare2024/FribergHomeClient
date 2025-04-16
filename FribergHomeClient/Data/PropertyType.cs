namespace FribergHomeClient.Data
{
    // Author: Christoffer
    public static class PropertyTypes
    {
        public enum PropertyType
        {
            None = 0,
            Condo,
            TownHouse,
            House,
            VacationHouse
        }

        public static List<PropertyType> PropertyTypeList()
        {
            return Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>().ToList();
        }

        public static string GetLocalized(PropertyType type)
        {
            return type switch
            {
                PropertyType.Condo => "Lägenhet",
                PropertyType.TownHouse => "Radhus",
                PropertyType.House => "Hus",
                PropertyType.VacationHouse => "Fritidshus",
                _ => "Ej vald",
            };
        }
    }
}

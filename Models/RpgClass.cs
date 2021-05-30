using System.Text.Json.Serialization;

namespace webapi_dotnet5.Models
{
    // to show the enum as string data instead of the correspoding numbers
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}
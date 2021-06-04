namespace webapi_dotnet5.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        //foreign key referencing Characters table
        
        public Character Character { get; set; }
        // this must be added to define that it is one-to-one reference Character Class
        public int CharacterId { get; set; }

    }
}
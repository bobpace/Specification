namespace Testing.Repository.Examples
{
    public class Animal : IEntity
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public bool IsSpecial { get; set;}
        public int NumberOfLegs { get; set;}
    }
}
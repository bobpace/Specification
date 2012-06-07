namespace Testing.Repository.Tests
{
    public class FakeEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsSpecial { get; set; }
    }
}
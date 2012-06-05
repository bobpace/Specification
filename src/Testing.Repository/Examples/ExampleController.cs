using Testing.Repository.Specifications;

namespace Testing.Repository.Examples
{
    public class ExampleController
    {
        readonly IRepository _repository;

        public ExampleController(IRepository repository)
        {
            _repository = repository;
        }

        public ExampleViewModel GetSpecialFourLeggedAnimals()
        {
            var fourLegged = new NumberOfLegsSpecification(4);
            var isSpecial = new LambdaSpecification<Animal>(x => x.IsSpecial);
            var results = _repository.Query(fourLegged.And(isSpecial));
            return new ExampleViewModel
            {
                Results = results
            };
        }
    }
}
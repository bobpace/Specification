using System.Collections.Generic;
using System.Linq;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;
using Testing.Repository.Examples;

namespace Testing.Repository.Tests.Examples
{
    public class ExampleControllerTests : InteractionContext<ExampleController>
    {
        IRepository _repository;
        List<Animal> _results;

        protected override void beforeEach()
        {
            _repository = MockFor<IRepository>();
            _results = new List<Animal>
            {
                new Animal(),
                new Animal(),
                new Animal(),
            };
        }

        [Test]
        public void Get_Special_Four_Legged_Animals_Queries_Repository_Using_Specification()
        {
            _repository.Expect(x => x.Query(Arg<ISpecification<Animal>>.Is.Anything))
                .Return(_results.AsQueryable());
            var viewModel = ClassUnderTest.GetSpecialFourLeggedAnimals();
            _repository.VerifyAllExpectations();
            Assert.IsTrue(viewModel.Results.SequenceEqual(_results));
        }
    }
}
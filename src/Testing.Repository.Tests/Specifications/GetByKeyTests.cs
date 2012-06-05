using NUnit.Framework;
using StructureMap.Pipeline;
using Testing.Repository.Specifications;

namespace Testing.Repository.Tests.Specifications
{
    public class GetByKeyTests : GenericInteractionContext<GetByKeySpecification<FakeEntity>>
    {
        int _key;

        protected override void beforeEach()
        {
            _key = 1;
            base.beforeEach();
        }

        public override void ConfigureDependencies(SmartInstance<GetByKeySpecification<FakeEntity>> instance)
        {
            instance.Ctor<int>().Is(_key);
        }

        [Test]
        public void When_Key_Matches_Returns_True()
        {
            var entity = new FakeEntity { Id = _key };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsTrue(result);
        }

        [Test]
        public void When_Key_Doesnt_Match_Return_False()
        {
            var entity = new FakeEntity { Id = 2 };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsFalse(result);
        }
    }
}
using System;
using System.Linq.Expressions;
using NUnit.Framework;
using StructureMap.Pipeline;
using Testing.Repository.Specifications;

namespace Testing.Repository.Tests.Specifications
{
    public class NegatedTests : GenericInteractionContext<NegatedSpecification<FakeEntity>>
    {
        NameIsCertainLengthSpecification _nameIs5LettersLongSpecification;

        protected override void beforeEach()
        {
            _nameIs5LettersLongSpecification = new NameIsCertainLengthSpecification(5);
            base.beforeEach();
        }

        public override void ConfigureDependencies(SmartInstance<NegatedSpecification<FakeEntity>> instance)
        {
            instance.Ctor<ISpecification<FakeEntity>>().Is(_nameIs5LettersLongSpecification);
        }

        [Test]
        public void When_Criteria_Is_Met_Returns_False()
        {
            var entity = new FakeEntity { Name = "Apple" };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsFalse(result);
        }

        [Test]
        public void When_Criteria_Is_Not_Met_Returns_True()
        {
            var entity = new FakeEntity { Name = "Apples" };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsTrue(result);
        }

        private class NameIsCertainLengthSpecification : Specification<FakeEntity>
        {
            readonly int _length;

            public NameIsCertainLengthSpecification(int length)
            {
                _length = length;
            }

            public override Expression<Func<FakeEntity, bool>> IsSatisfied()
            {
                return x => x.Name.Length == _length;
            }
        }
    }
}
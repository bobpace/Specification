using System;
using System.Linq.Expressions;
using NUnit.Framework;
using StructureMap.Pipeline;
using Testing.Repository.Specifications;

namespace Testing.Repository.Tests.Specifications
{
    public class AndTests : GenericInteractionContext<AndSpecification<FakeEntity>>
    {
        MinimumAgeSpecification _minimumAgeSpecification;
        MaximumAgeSpecification _maximumAgeSpecification;

        protected override void beforeEach()
        {
            _minimumAgeSpecification = new MinimumAgeSpecification(21);
            _maximumAgeSpecification = new MaximumAgeSpecification(30);
            base.beforeEach();
        }

        public override void ConfigureDependencies(SmartInstance<AndSpecification<FakeEntity>> instance)
        {
            instance.Ctor<ISpecification<FakeEntity>>("first").Is(_minimumAgeSpecification);
            instance.Ctor<ISpecification<FakeEntity>>("second").Is(_maximumAgeSpecification);
        }

        [Test]
        public void When_Only_First_Criteria_Is_Met_Returns_False()
        {
            var entity = new FakeEntity { Age = 50 };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsFalse(result);
        }

        [Test]
        public void When_Only_Second_Criteria_Is_Met_Returns_False()
        {
            var entity = new FakeEntity { Age = 10 };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsFalse(result);
        }

        [Test]
        public void When_Both_Criteria_Are_Met_Returns_True()
        {
            var entity = new FakeEntity { Age = 25 };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsTrue(result);
        }

        private class MaximumAgeSpecification : Specification<FakeEntity>
        {
            readonly int _maximumAge;

            public MaximumAgeSpecification(int maximumAge)
            {
                _maximumAge = maximumAge;
            }

            public override Expression<Func<FakeEntity, bool>> IsSatisfied()
            {
                return x => x.Age <= _maximumAge;
            }
        }

        private class MinimumAgeSpecification : Specification<FakeEntity>
        {
            readonly int _minimumAge;

            public MinimumAgeSpecification(int minimumAge)
            {
                _minimumAge = minimumAge;
            }

            public override Expression<Func<FakeEntity, bool>> IsSatisfied()
            {
                return x => x.Age >= _minimumAge;
            }
        }
         
    }
}
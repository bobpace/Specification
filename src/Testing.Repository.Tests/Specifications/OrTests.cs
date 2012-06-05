using System;
using System.Linq.Expressions;
using NUnit.Framework;
using StructureMap.Pipeline;
using Testing.Repository.Specifications;

namespace Testing.Repository.Tests.Specifications
{
    public class OrTests : GenericInteractionContext<OrSpecification<FakeEntity>>
    {
        NameStartsWithSpecification _nameStartsWithTestSpecification;
        NameEndsWithSpecification _nameEndsWithModelSpecification;

        protected override void beforeEach()
        {
            _nameStartsWithTestSpecification = new NameStartsWithSpecification("Test");
            _nameEndsWithModelSpecification = new NameEndsWithSpecification("Model");
            base.beforeEach();
        }

        public override void ConfigureDependencies(SmartInstance<OrSpecification<FakeEntity>> instance)
        {
            instance.Ctor<ISpecification<FakeEntity>>("first").Is(_nameStartsWithTestSpecification);
            instance.Ctor<ISpecification<FakeEntity>>("second").Is(_nameEndsWithModelSpecification);
        }

        [Test]
        public void When_Only_First_Criteria_Is_Met_Returns_True()
        {
            var entity = new FakeEntity { Name = "TestSomething" };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsTrue(result);
        }

        [Test]
        public void When_Only_Second_Criteria_Is_Met_Returns_True()
        {
            var entity = new FakeEntity { Name = "SomethingModel" };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsTrue(result);
            
        }

        [Test]
        public void When_Both_Criteria_Are_Met_Returns_True()
        {
            var entity = new FakeEntity { Name = "TestSomethingModel" };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsTrue(result);
        }

        [Test]
        public void When_Neither_Criteria_Are_Met_Returns_False()
        {
            var entity = new FakeEntity { Name = "Something" };
            var expression = ClassUnderTest.IsSatisfied();
            var result = expression.Compile().Invoke(entity);
            Assert.IsFalse(result);
        }

        private class NameStartsWithSpecification : Specification<FakeEntity>
        {
            readonly string _startsWith;

            public NameStartsWithSpecification(string startsWith)
            {
                _startsWith = startsWith;
            }

            public override Expression<Func<FakeEntity, bool>> IsSatisfied()
            {
                return x => x.Name.StartsWith(_startsWith);
            }
        }

        private class NameEndsWithSpecification : Specification<FakeEntity>
        {
            readonly string _endsWith;

            public NameEndsWithSpecification(string endsWith)
            {
                _endsWith = endsWith;
            }

            public override Expression<Func<FakeEntity, bool>> IsSatisfied()
            {
                return x => x.Name.EndsWith(_endsWith);
            }
        }
    }
}
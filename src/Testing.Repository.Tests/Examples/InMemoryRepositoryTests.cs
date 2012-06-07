using System.Collections.Generic;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace Testing.Repository.Tests.Examples
{
    public class InMemoryRepositoryTests : InteractionContext<InMemoryRepository>
    {
        IObjectContext _objContext;
        HashSet<FakeEntity> _fakeHash;

        protected override void beforeEach()
        {
            _objContext = MockFor<IObjectContext>();
            _fakeHash = new HashSet<FakeEntity>
            {
                new FakeEntity {Age = 12, Id = 3, IsSpecial = false, Name = "Billy"}
            };
            _objContext.Expect(x => x.GetObjects<FakeEntity>()).Return(_fakeHash);
        }

        [Test]
        public void Demo()
        {
            var entity = new FakeEntity { Id = 100};
            _objContext.Expect(x => x.Add(Arg.Is(entity)))
                .Callback<FakeEntity>(_fakeHash.Add);
            ClassUnderTest.Add(entity);

            var result = ClassUnderTest.GetByKey<FakeEntity>(100);
            Assert.IsNotNull(result);

            _objContext.VerifyAllExpectations();
        }
        
    }
}
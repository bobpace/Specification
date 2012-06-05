using FubuTestingSupport;
using StructureMap.Pipeline;

namespace Testing.Repository.Tests
{
    public class GenericInteractionContext<T> : InteractionContext<T> where T : class
    {
        protected override void beforeEach()
        {
            Container.Configure(x =>
            {
                var instance = x.For<T>().Use<T>();
                ConfigureDependencies(instance);
            });
            base.beforeEach();
        }

        public virtual void ConfigureDependencies(SmartInstance<T> instance)
        {
        }
    }
}
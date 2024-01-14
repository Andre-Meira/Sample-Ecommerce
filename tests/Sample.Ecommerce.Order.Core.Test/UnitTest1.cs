using Sample.Ecommerce.Core.Domain.Stream;

namespace Sample.Ecommerce.Order.Core.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            new TesteEvent();

            var a = EventStream.Initialized(new TesteEvent());

            var b = a.Name;

            b = "teste";

        }
    }

    public class TesteEvent : IEventData
    {
        public Guid IdCorrelation { get ; init ; }

        public string testee { get ; init ; }

        public TesteEvent()
        {
            IdCorrelation = Guid.NewGuid();
            testee = IdCorrelation.ToString();
        }
    }
}
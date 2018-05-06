namespace StructureMap.AutoMocking.Moq
{
    using global::Moq;

    public abstract class ForSut<TSut> where TSut : class
    {
        protected ForSut()
        {
            Setup();
        }

        protected MoqAutoMocker<TSut> Mocker { get; private set; }

        protected TSut Sut => Mocker.ClassUnderTest;

        private void Setup()
        {
            Mocker = new MoqAutoMocker<TSut>();

            ConfigureIoC(Mocker.Container);
        }

        protected Mock<TInterface> GetMockFor<TInterface>() where TInterface : class
        {
            return Mock.Get(Mocker.Get<TInterface>());
        }

        protected virtual void ConfigureIoC(IContainer container)
        {
        }
    }
}
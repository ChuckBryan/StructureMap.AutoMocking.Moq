namespace PalmettoSoft.StructureMap.AutoMocking.Moq
{
    using global::StructureMap.AutoMocking;

    public class MoqAutoMocker<T> : AutoMocker<T> where T: class
    {
        public MoqAutoMocker() : base(new MoqServiceLocator())
        {
            ServiceLocator = new MoqServiceLocator();
        }
    }
}
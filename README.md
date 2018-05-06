# PalmettoSoft StructureMap AutoMocking for Moq
StructureMap's AutoMocking feature is one of my favorite Unit Testing Tools. Consider that you are writing tests and your SUT has two dependencies: one you care about and one that you don't (e.g. logging). Normally, you would need to still create a mock of your logger so that you could satisfy the constuctor. Not with AutoMocking. Consider the following base class:
```c#
public abstract class ForSut<TSut>  where TSut:class
{
    protected ForSut()
    {
        Mocker = new MoqAutoMocker<TSut>();
    }

    public MoqAutoMocker<TSut> Mocker { get; }

    public TSut Sut => Mocker.ClassUnderTest;

    public Mock<TInterface> GetMockFor<TInterface>() where TInterface : class
    {
        return Mock.Get(Mocker.Get<TInterface>());
    }
}
```
The property Mocker is an instance of MoqAutoMocker<TSut>, where TSut is the System Under Test that we are testing. Here is an example of the Sut:

```c#
public class SystemUnderTest
{
    private readonly ISomeDependency _someDependency;

    public SystemUnderTest(ISomeDependency someDependency)
    {
        _someDependency = someDependency;
    }

    public void DoWork()
    {
        _someDependency.LoadData();
    }
}
```
Nothing complicated here - just a single method that delegates to some dependency that will load data.

Using the base ForSpec<TSut> we can create the xUnit Test:

```c#
public class UnitTest1 : ForSut<SystemUnderTest>
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var mockDependecy = GetMockFor<ISomeDependency>();

        mockDependecy.Setup(x => x.LoadData());

        // Act
        Sut.DoWork();
        
        // Assert
        mockDependecy.VerifyAll();
    }
}
```
Using the helper GetMockFor, we can setup expectations on the mock dependency. Then, we call the method on the Sut that we are testing and then verify the expectations (in this case, that some dependency actually called the LoadData method.


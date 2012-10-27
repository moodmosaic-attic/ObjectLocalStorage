using Ploeh.AutoFixture.Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

public class sutTests
{
    [Fact]
    public void SutIsObjectLocalStorage()
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        // Exercise system and verify outcome
        Assert.IsAssignableFrom<IObjectLocalStorage>(sut);
        // Teardown
    }

    [Theory, AutoData]
    public void GetWhenSetIsNotInvokedForObjectReturnsCorrectResult(
        DateTime @object)
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        // Exercise system
        object result = sut.Get(@object);
        // Verify outcome
        Assert.Null(result);
        // Teardown
    }

    [Theory]
    [InlineAutoData(-10)]
    [InlineAutoData(2.0)]
    [InlineAutoData("3")]
    public void GetReturnsCorrectResult(
        object @object,
        string expected)
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        // Exercise system
        sut.Set(@object, expected);
        object result = sut.Get(@object);
        // Verify outcome
        Assert.Equal(expected, result);
        // Teardown
    }

    [Theory, AutoData]
    public void GetReturnsCorrectResultMultipleCall(
        object @object,
        string dummy,
        double expected)
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        Array.ForEach(new int[3], x => sut.Set(@object, dummy));
        // Exercise system
        sut.Set(@object, expected);
        // Verify outcome
        object result = sut.Get(@object);
        // Teardown
        Assert.Equal(expected, result);
    }

    [Theory, InlineData(null)]
    public void GetForNullReferenceReturnsCorrectResult(object @object)
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        // Exercise system
        object result = sut.Get(@object);
        // Verify outcome
        Assert.Null(result);
        // Teardown
    }

    [Theory]
    [InlineAutoData(-10)]
    [InlineAutoData(2.0)]
    [InlineAutoData("3")]
    public void GetReturnsCorrectResultAsynchronousCall(
        object @object,
        string expected)
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        sut.Set(@object, expected);
        object result = null;
        // Exercise system
        new Task(() => result = sut.Get(@object)).Start();
        SpinWait.SpinUntil(() => result != null);
        // Verify outcome
        Assert.Equal(expected, result);
        // Teardown
    }

    [Theory]
    [InlineAutoData(-10)]
    [InlineAutoData(2.0)]
    [InlineAutoData("3")]
    public void GetWhenSetIsInvokedForDifferentObjectReturnsCorrectResult(
        object dummy,
        object @object,
        string expected)
    {
        // Fixture setup
        var sut = new ObjectLocalStorage();
        sut.Set(dummy, expected);
        // Exercise system
        object result = sut.Get(@object);
        // Verify outcome
        Assert.Null(result);
        // Teardown
    }
}
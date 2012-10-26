using Ploeh.AutoFixture.Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

public class ObjectLocalStorageTests
{
    [Theory, AutoData]
    public void GetWhenSetIsNotInvokedForObjectReturnsCorrectResult(
        DateTime @object)
    {
        // Fixture setup
        // Exercise system
        object result = ObjectLocalStorage.Get(@object);
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
        // Exercise system
        ObjectLocalStorage.Set(@object, expected);
        object result = ObjectLocalStorage.Get(@object);
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
        Array.ForEach(new int[3], x => ObjectLocalStorage.Set(@object, dummy));
        // Exercise system
        ObjectLocalStorage.Set(@object, expected);
        // Verify outcome
        object result = ObjectLocalStorage.Get(@object);
        // Teardown
        Assert.Equal(expected, result);
    }

    [Theory, InlineData(null)]
    public void GetForNullReferenceReturnsCorrectResult(object @object)
    {
        // Fixture setup
        // Exercise system
        object result = ObjectLocalStorage.Get(@object);
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
        ObjectLocalStorage.Set(@object, expected);
        object result = null;
        // Exercise system
        new Task(() => result = ObjectLocalStorage.Get(@object)).Start();
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
        ObjectLocalStorage.Set(dummy, expected);
        // Exercise system
        object result = ObjectLocalStorage.Get(@object);
        // Verify outcome
        Assert.Null(result);
        // Teardown
    }
}
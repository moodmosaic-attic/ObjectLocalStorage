using System.Runtime.CompilerServices;

/// <summary>
/// Allows to store an arbitrary piece of data to objects using the 
/// <see cref="ConditionalWeakTable" /> class.
/// </summary>
public class ObjectLocalStorage : IObjectLocalStorage
{
    private static readonly ConditionalWeakTable<object, object> cwt =
        new ConditionalWeakTable<object, object>();

    /// <summary>
    /// Removes the associated data for this object from the 
    /// <see cref="ConditionalWeakTable" /> and then associates the data with
    /// this object.
    /// </summary>
    /// <param name="object">The object that we would like to associate an 
    /// arbitrary piece of data.
    /// </param>
    /// <param name="value">An arbitrary piece of data.</param>
    public void Set(object @object, object value)
    {
        cwt.Remove(@object);
        cwt.Add(@object, value);
    }

    /// <summary>
    /// Gets the data associated for a given object from the 
    /// <see cref="ConditionalWeakTable" />.
    /// </summary>
    /// <param name="object">The object that we have previously associated 
    /// some data.
    /// </param>
    /// <returns></returns>
    public object Get(object @object)
    {
        if (@object == null)
            return null;

        object value;
        cwt.TryGetValue(@object, out value);
        return value;
    }
}
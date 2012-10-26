using System.Runtime.CompilerServices;

/// <summary>
/// Allows to store an arbitrary piece of data to objects.
/// </summary>
public static class ObjectLocalStorage
{
    private static readonly ConditionalWeakTable<object, object> cwt =
        new ConditionalWeakTable<object, object>();

    /// <summary>
    /// Removes the associated data for this object from the store (if exists)
    /// and then associates the data with this object.
    /// </summary>
    /// <param name="object">The object that we would like to associate an 
    /// arbitrary piece of data.
    /// </param>
    /// <param name="value">An arbitrary piece of data.</param>
    public static void Set(object @object, object value)
    {
        cwt.Remove(@object);
        cwt.Add(@object, value);
    }

    /// <summary>
    /// Gets the data associated for a given object.
    /// </summary>
    /// <param name="object">The object that we have previously associated 
    /// some data.
    /// </param>
    /// <returns></returns>
    public static object Get(object @object)
    {
        if (@object == null)
            return null;

        object value;
        cwt.TryGetValue(@object, out value);
        return value;
    }
}
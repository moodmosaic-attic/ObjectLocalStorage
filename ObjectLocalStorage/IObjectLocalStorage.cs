using System;

/// <summary>
/// Allows to store an arbitrary piece of data to objects.
/// </summary>
public interface IObjectLocalStorage
{
    /// <summary>
    /// Removes the associated data for this object from the store (if exists)
    /// and then associates the data with this object.
    /// </summary>
    /// <param name="object">The object that we would like to associate an 
    /// arbitrary piece of data.
    /// </param>
    /// <param name="value">An arbitrary piece of data.</param>
    void Set(object @object, object value);

    /// <summary>
    /// Gets the data associated for a given object.
    /// </summary>
    /// <param name="object">The object that we have previously associated 
    /// some data.
    /// </param>
    /// <returns></returns>
    object Get(object @object);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Serializer
{
    /// <summary>
    /// Defines methods for serializing and deserializing an inventory.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes the specified inventory container to a string.
        /// </summary>
        /// <param name="container">The inventory container to serialize.</param>
        /// <returns>A string representation of the serialized inventory container.</returns>
        string Serialize(Inventory container);

        /// <summary>
        /// Asynchronously serializes the specified inventory container to a string.
        /// </summary>
        /// <param name="container">The inventory container to serialize.</param>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a string representation of the serialized inventory container.
        /// </returns>
        Task<string> SerializeAsync(Inventory container);

        /// <summary>
        /// Deserializes the specified string to an inventory container.
        /// </summary>
        /// <param name="value">The string representation of the inventory container to deserialize.</param>
        /// <returns>The deserialized inventory container.</returns>
        Inventory Deserialize(string value);

        /// <summary>
        /// Asynchronously deserializes the specified string to an inventory container.
        /// </summary>
        /// <param name="value">The string representation of the inventory container to deserialize.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the deserialized inventory container.
        /// </returns>
        Task<Inventory> DeserializeAsync(string value);
    }
}

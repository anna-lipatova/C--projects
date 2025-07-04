using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace UniversalInventorySystemLibrary.Serializer
{
    /// <summary>
    /// Provides methods for serializing and deserializing 
    /// inventory objects to and from XML.
    /// </summary>
    public class XMLSerializer: ISerializer
    {
        public string Serialize(Inventory inventory)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, inventory);
                return textWriter.ToString();
            }
        }

        public async Task<string> SerializeAsync(Inventory inventory)
        {
            return await Task.Run(() => Serialize(inventory));
        }

        public Inventory Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Inventory));
            using (StringReader textReader = new StringReader(xml))
            {
                return (Inventory)serializer.Deserialize(textReader);
            }
        }

        public async Task<Inventory> DeserializeAsync(string xml)
        {
            return await Task.Run(() => Deserialize(xml));
        }
    }
}

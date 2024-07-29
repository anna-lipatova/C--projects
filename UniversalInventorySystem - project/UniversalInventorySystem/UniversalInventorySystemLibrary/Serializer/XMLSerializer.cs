using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using UniversalInventorySystemLibrary.Container;

namespace UniversalInventorySystemLibrary.Serializer
{
    public class XMLSerializer: ISerializer
    {
        public string Serialize(IItemContainer container)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<IItem>));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, container.GetItems());
                return textWriter.ToString();
            }
        }

        public async Task<string> SerializeAsync(IItemContainer container)
        {
            return await Task.Run(() => Serialize(container));
        }

        public List<IItem> Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<IItem>));
            using (StringReader textReader = new StringReader(xml))
            {
                return (List<IItem>)serializer.Deserialize(textReader);
            }
        }

        public async Task<List<IItem>> DeserializeAsync(string xml)
        {
            return await Task.Run(() => Deserialize(xml));
        }
    }
}

using System;
using System.IO;
using System.Xml.Serialization;


namespace RMQ.Core
{
    public static class MessageSerializer
    {
        public static Message FromXml(string xml)
        {
            using (TextReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = GetSerializer();

                return (Message)serializer.Deserialize(reader);
            }
        }


        public static string ToXml(Message value)
        {
            using (var writer = new StringWriter())
            {
                XmlSerializer serializer = GetSerializer();
                serializer.Serialize(writer, value, GetXmlSerializerNamespaces());

                return writer.ToString();
            }
        }


        private static XmlSerializer GetSerializer()
        {
            var serializer = new XmlSerializer(typeof(Message));
            return serializer;
        }


        private static XmlSerializerNamespaces GetXmlSerializerNamespaces()
        {
            var serializerNamespaces = new XmlSerializerNamespaces();
            serializerNamespaces.Add(string.Empty, string.Empty);

            return serializerNamespaces;
        }
    }
}

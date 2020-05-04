using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xpinn.Util.PaymentACH;

/// <summary>
/// Summary description for DynamicFields
/// </summary>
public class DynamicFields : Hashtable, IXmlSerializable
{
    public void Create(string fields)
    {
        foreach (string field in fields.Split(new char[] { ';' }))
            this[field] = "";
    }

    public string SaveToXML()
    {
        StringWriter writer = new StringWriter();
        XmlSerializer serial = new XmlSerializer(this.GetType());
        serial.Serialize(writer, this);
        writer.Close();
        return writer.ToString();
    }

    public static DynamicFields LoadFromXML(string xml)
    {
        XmlSerializer serial = new XmlSerializer(typeof(DynamicFields));
        StringReader reader = new StringReader(xml);
        DynamicFields fields = (DynamicFields)serial.Deserialize(reader);
        reader.Close();
        return fields;
    }

    public PSEHostingField[] SaveAsPSEHostingFields()
    {
        PSEHostingField[] fields = new PSEHostingField[this.Keys.Count];
        int c = 0;
        foreach (string key in this.Keys)
        {
            fields[c] = new PSEHostingField();
            fields[c].Name = key;
            fields[c].Value = this[key];
            c++;
        }
        return fields;
    }

    #region IXmlSerializable Members
    public System.Xml.Schema.XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(System.Xml.XmlReader reader)
    {
        reader.Read();
        reader.ReadStartElement("fields");
        while (reader.NodeType != XmlNodeType.EndElement)
        {
            reader.ReadStartElement("field");
            string key = reader.ReadElementString("key");
            string value = reader.ReadElementString("value");
            reader.ReadEndElement();
            reader.MoveToContent();
            this.Add(key, value);
        }
        reader.ReadEndElement();
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
        writer.WriteStartElement("fields");
        foreach (object key in this.Keys)
        {
            object value = this[key];
            writer.WriteStartElement("field");
            writer.WriteElementString("key", key.ToString());
            writer.WriteElementString("value", value.ToString());
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
    }

    #endregion

}
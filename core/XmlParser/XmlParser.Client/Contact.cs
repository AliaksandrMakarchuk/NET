using System;
using System.Xml.Serialization;

namespace XmlParser.Client
{
    [Serializable]
    public class Contact
    {
        public string Name { get; set; }
        public string Phone {get;set;}
    }
}
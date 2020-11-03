using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlParser.Client
{
    [XmlRoot(Namespace = "www.contoso.com")]
    public class PhoneBook
    {
        public List<Contact> Contacts { get; set; }
    }
}
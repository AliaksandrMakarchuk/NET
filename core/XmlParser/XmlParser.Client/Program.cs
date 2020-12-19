using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace XmlParser.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            UseXmlReader();
            // WorkWithPhonebook();
        }

        private static void UseXmlReader()
        {
            XmlReader r = XmlReader.Create(GetXmlFilePath("books.xml"));
            while (r.NodeType != XmlNodeType.Element)
            {
                r.Read();
            }

            XElement e = XElement.Load(r);
            Console.WriteLine(e);
        }

        private static void WorkWithPhonebook()
        {
            var path = GetXmlFilePath("phonebook.xml");

            PhoneBook phoneBook = null;
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var formatter = new XmlSerializer(typeof(PhoneBook));
                try
                {
                    phoneBook = (PhoneBook)formatter.Deserialize(fs);
                }
                catch
                {
                    phoneBook = new PhoneBook();
                }
            }

            if (phoneBook.Contacts != null)
            {
                Console.WriteLine($"Existing contacts:");
                foreach (var contact in phoneBook.Contacts)
                {
                    Console.WriteLine($"{contact.Name}: {contact.Phone}");
                }
            }
            else
            {
                phoneBook.Contacts = new List<Contact>();
            }

            var contacts = new List<Contact>();

            var flag = true;
            string input;
            do
            {
                Console.WriteLine($"Input contact name");
                Console.Write(">");
                var name = Console.ReadLine();
                flag = flag && !string.IsNullOrWhiteSpace(name);

                Console.WriteLine($"Input contact phone number");
                Console.Write(">");
                var number = Console.ReadLine();
                flag = flag && !string.IsNullOrWhiteSpace(name);

                if (flag)
                {
                    contacts.Add(new Contact { Name = name, Phone = number });
                }
            }
            while (flag);

            phoneBook.Contacts.AddRange(contacts);

            using (var fs = new FileStream(path, FileMode.Truncate))
            {
                var formatter = new XmlSerializer(typeof(PhoneBook));

                formatter.Serialize(fs, phoneBook);
            }
        }

        private static XElement LoadXml()
        {
            var xmlFilePath = GetXmlFilePath("phonebook.xml");

            XElement root = XElement.Load(xmlFilePath);

            var items = root.Descendants("Item");

            foreach (var item in items)
            {
                var name = (string)item.Element("Name");
                Console.WriteLine($"Item: {name}");
            }

            return root;
        }

        private static string GetXmlFilePath(string xmlName)
        {
            var filename = xmlName;
            var currentDirectory = Directory.GetCurrentDirectory();

            return Path.Combine(currentDirectory, filename);
        }

        private static void AddRecords(XElement root)
        {
            var newItems = new XElement("Root",
            new XElement("Item", new XElement("Name", "Alex1")),
            new XElement("Item", new XElement("Name", "Alex2")),
            new XElement("Item", new XElement("Name", "Alex3")),
            new XElement("Item", new XElement("Name", "Alex4"))
            );

            root.Add(newItems.Elements());
            root.Save(GetXmlFilePath("phonebook.xml"));
        }
    }
}

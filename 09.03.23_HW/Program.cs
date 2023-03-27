using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using serializer;
using System.Runtime.Serialization.Json;

namespace serializer
{
    [Serializable]
    [DataContract]
    public class Phone
    {
        [DataMember]
        public string Imya { get; set; }

        [DataMember]
        public int Zena { get; set; }
        
        [DataMember]
        public int Data { get; set; }

        [DataMember]
        public string Creator { get; set; }
        public Phone(string imya, int zena, string creator, int data)
        {
            Imya = imya;
            Zena = zena;
            Creator = creator;
            Data = data;
        }
        public Phone() { }

        public override string ToString()
        {
            return $"Telephone: {Creator}\nModel: {Imya}\nPrice: {Zena}\nDate: {Data}\n\n";
        }
    }
}
namespace _09._03._23_HW
{
    internal class Program
    {
        #region XML_Func
        public static void Add_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            e.Add(new XElement("phone",
                new XAttribute("name", "A72"),
                new XElement("creator", "Samsung"),
                new XElement("price", "12000"),
                new XElement("date", "2020")));
            x.Save(temp);
        }

        public static void Search_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            var search = x.Element("phone")?
                .Elements("phone")?
                .FirstOrDefault(p => p.Attribute("name")?.Value == "A72");
            var name_s = search?.Attribute("name")?.Value;
            var company_s = search?.Element("creator")?.Value;
            var price_s = search?.Element("price")?.Value;
            var date_s = search?.Element("date")?.Value;
            Console.WriteLine($"Name: {name_s}  Age: {company_s}  Price: {price_s}  Date: {date_s}");
        }

        public static void Edit_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            var tom = x.Element("phone")?
                .Elements("phone")
                .FirstOrDefault(p => p.Attribute("name")?.Value == "A72");

            if (tom != null)
            {
                var name_e = tom.Attribute("name");
                if (name_e != null) name_e.Value = "iPhone 13";
                var company_e = tom.Element("creator");
                if (company_e != null) company_e.Value = "Apple";
                var price_e = tom.Element("price");
                if (company_e != null) company_e.Value = "40000";
                var date_e = tom.Element("date");
                if (company_e != null) company_e.Value = "2021";
                x.Save(temp);
            }
        }

        public static void Show_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            var search = x.Element("phone")?
                .Elements("phone")?
                .FirstOrDefault(p => p.Attribute("name")?.Value == "A72");
            var name_sh = search?.Attribute("name")?.Value;
            var company_sh = search?.Element("creator")?.Value;
            var price_sh = search?.Element("price")?.Value;
            var date_sh = search?.Element("date")?.Value;
            Console.WriteLine($"Name: {name_sh}  Age: {company_sh}  Price: {price_sh}  Date: {date_sh}");
        }

        public static void Delete_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            if (e != null)
            {
                var bob = e.Elements("phone")
                    .FirstOrDefault(p => p.Attribute("name")?.Value == "A72");
                if (bob != null)
                {
                    bob.Remove();
                    x.Save(temp);
                }
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Phone p = new Phone("M21", 42, "Xiomo", 2021);
            Phone p2 = new Phone("412", 42, "53311", 2021);
            List<Phone> list = new List<Phone>();
            list.Add(p);
            list.Add(p2);
            FileStream stream = new FileStream("../../data.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Phone>));
            serializer.Serialize(stream, list);
            stream.Close();
            stream = new FileStream("../../data.xml", FileMode.Open);
            list = (List<Phone>)serializer.Deserialize(stream);
            string s = String.Empty;
            foreach (Phone j in list)
                s += j.ToString();
            Console.WriteLine(s);
            stream.Close();

            stream = new FileStream("../../list.json", FileMode.Create);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Phone>));
            jsonFormatter.WriteObject(stream, list);
            stream.Close();
            stream = new FileStream("../../list.json", FileMode.Open);
            list = (List<Phone>)jsonFormatter.ReadObject(stream);
            s = String.Empty;
            foreach (Phone j in list)
                s += j.ToString();
            Console.WriteLine(s);
            stream.Close();

        }
    }
}

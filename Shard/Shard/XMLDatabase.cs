using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Shard
{
    public class XMLDatabase
    {
        private XDocument doc;
        private string path;
        public XMLDatabase(String path, String root = "objects")
        {
            this.path = path;
            try
            {
                doc = XDocument.Load(path);
            }
            catch (XmlException exception)
            {
                Console.WriteLine(exception.GetBaseException());
                doc = new XDocument(new XElement(root));
            }
        }

        //returns the document for reading
        public XDocument getDocument()
        {
            return doc;
        }

        //reloads the document
        public void reload()
        {
            doc = XDocument.Load(path);
        }

        //saves the document
        public void save()
        {
            doc.Save(path);
        }

        //checks if there are any nodes in the document
        public bool isEmpty()
        {
            return !doc.Root.Elements().Any();
        }

        /*adds a node to the document with the next available Id
        If IDs 1,3,4 are valid, it will use id 2.
        Assumes elements are in order
        Using to prevent A) same IDs and B) excess number of IDs
        RECYCLING! YAY!
        
        Example nodes to be added:

        <user>
            <username>user</username>
            <password>pass</password>
        </user>

        <object>
            <hp>100</hp>
            <x>1241.1</x>
            <y>415.1</y>
        <object>

        Ids will be applied when added

        */

        public void addNode(XElement newNode)
        {
            if (!doc.Root.HasElements)
            {
                newNode.SetAttributeValue("id", 0);
                doc.Root.Add(newNode);
            }
            else
            {
                int count = 1;
                foreach (XElement e in doc.Root.Elements())
                {
                    if (Convert.ToInt32(e.Attribute("id").Value) != count - 1) //If Id != to the count in the list
                    {
                        newNode.SetAttributeValue("id", (count - 1).ToString());
                        doc.Root.Elements() //Gets all the elements "id" in an array
                        .Where(id => id == e).FirstOrDefault()
                        .AddBeforeSelf(newNode); //adds newNode after that element, with an Id of count
                        break;
                    }
                    else if (count == doc.Root.Elements().Count())
                    {
                        newNode.SetAttributeValue("id", count.ToString());
                        doc.Root.Elements() //Gets all the elements "id" in an array
                        .Where(id => id == e).FirstOrDefault() //Gets last element in a row with matching id and count
                        .AddAfterSelf(newNode); //adds newNode after that element, with an Id of count
                        break;
                    }

                    count++;
                }
            }
            this.save();
        }

        public void removeNode(int id)
        {
            foreach (XElement e in doc.Root.Elements())
            {
                if (Convert.ToInt32(e.Attribute("id").Value) == id)
                {
                    doc.Root.Elements()
                    .Where(x => x == e).FirstOrDefault()
                    .Remove();
                }
            }
            this.save();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kleinprojekt_Adressbuch_OOP__in_WPF
{
    class Addressbook
    {
        //Variables
        private List<Contact> addressBook = new List<Contact>();

        public int Count()
        {
            int count;
            count = addressBook.Count();
            return count;
        }

        public string CreateListItem(int index)
        {
            string item;
            item = addressBook[index].CreateItem();
            return item;
        }

        public bool FileExists(string filename)
        {
            bool fileExists = true;
            if (!File.Exists(filename)) fileExists = false;
            return fileExists;
        }

        //Constructors
        public Addressbook()
        {
        }

        //Methods
        public void AddContact(string surname, string name, string phoneNumber)
        {
            Contact person = new Contact(surname, name, phoneNumber);
            addressBook.Add(person);
        }

        public void RemoveContact(int index)
        {
            addressBook.RemoveAt(index);
        }

        public void EditContact(int index, string surname, string name, string phoneNumber)
        {
            addressBook[index].EditContact(surname, name, phoneNumber);
        }

        public void ClearBook()
        {
            addressBook.Clear();
        }

        public void ExportBook(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                for (int i = 0; i < addressBook.Count; i++)
                {
                    sw.WriteLine(addressBook[i].CreateItem());
                }
            }
        }

        public void ImportFile(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineSeperated = new string[3];
                    lineSeperated = line.Split('-');
                    Contact person = new Contact(lineSeperated[0], lineSeperated[1], lineSeperated[2]);
                    addressBook.Add(person);
                }
            }
        }
    }
}


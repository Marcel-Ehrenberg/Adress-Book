using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kleinprojekt_Adressbuch_OOP__in_WPF
{
    class Contact
    {
        //Variables
        private string surname;
        private string name;
        private string phoneNumber;

        public string CreateItem()
        {
            string adress = surname + "-" + name + "-" + phoneNumber;
            return adress;
        }

        //Constructors
        public Contact(string surname, string name, string phoneNumber)
        {
            this.surname = surname;
            this.name = name;
            this.phoneNumber = phoneNumber;
        }

        //Methods
        public void EditContact(string surname, string name, string phoneNumber)
        {
            this.surname = surname;
            this.name = name;
            this.phoneNumber = phoneNumber;
        }
    }
}

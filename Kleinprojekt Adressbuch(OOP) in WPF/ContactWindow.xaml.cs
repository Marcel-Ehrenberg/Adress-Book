using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kleinprojekt_Adressbuch_OOP__in_WPF
{
    /// <summary>
    /// Interaktionslogik für AddWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow(bool isAdd)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            if (isAdd)
            {
                WindowDesc.Text = "Add Contact";
            }
            else
            {
                WindowDesc.Text = "Edit Contact";
                this.contact = (Application.Current.MainWindow as MainWindow).SelectedContact();
                string[] contactSplit = this.contact.Split('-');
                SurnameEntry.Text = contactSplit[0];
                NameEntry.Text = contactSplit[1];
                PhoneNumberEntry.Text = contactSplit[2];
            }
            this.isAdd = isAdd;
        }

        private bool isAdd;
        private string contact;
        private string surname;
        private string name;
        private string phoneNumber;

        private void NewContact(string surname, string name, string phoneNumber)
        {
            this.surname = surname;
            this.name = name;
            this.phoneNumber = phoneNumber;
            contact = surname + " - " + name + " - " + phoneNumber;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            if (!mainWindow.AdressValidity(SurnameEntry.Text, false))
            {
                MessageBox.Show("Surname contains invalid characters.");
            }
            else if(!mainWindow.AdressValidity(NameEntry.Text, false))
            {
                MessageBox.Show("Name contains invalid characters.");
            }
            else if(!mainWindow.AdressValidity(PhoneNumberEntry.Text, true))
            {
                MessageBox.Show("Phonenumber contains invalid characters.");
            }
            else
            {
                NewContact(SurnameEntry.Text, NameEntry.Text, PhoneNumberEntry.Text);
                if(isAdd) mainWindow.Add(surname, name, phoneNumber, contact);
                else mainWindow.Edit(surname, name, phoneNumber, contact);
                Close();
            }
        }
    }
}

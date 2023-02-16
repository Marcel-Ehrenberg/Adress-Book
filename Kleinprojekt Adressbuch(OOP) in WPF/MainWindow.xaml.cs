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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Kleinprojekt_Adressbuch_OOP__in_WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        //Variables
        private int selection;
        private bool isSelected;
        private Addressbook addressBook = new Addressbook();

        public bool AdressValidity(string input, bool isNumber)
        {
            bool validity = true;
            if (input.Length == 0)
            {
                validity = false;
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsPunctuation(input[i]) || char.IsSymbol(input[i]) || char.IsSurrogate(input[i]))
                    {
                        validity = false;
                    }
                    else
                    {
                        if (isNumber == false)
                        {
                            if (char.IsDigit(input[i]))
                            {
                                validity = false;
                            }
                        }
                        else
                        {
                            if (char.IsLetter(input[i]))
                            {
                                validity = false;
                            }
                        }
                    }
                }
            }
            return validity;
        }

        public string SelectedContact()
        {
            return ContactList.Items.GetItemAt(selection).ToString();
        }

        //Methods
        public void ShowInList()
        {
            for (int i = 0; i < addressBook.Count(); i++)
            {
                string item = addressBook.CreateListItem(i);

                if (item.ToLower().Contains(SearchBar.Text))
                {
                    ContactList.Items.Add(item);
                }
            }
        }

        public void Export(string filename)
        {
            addressBook.ExportBook(filename);
        }

        public void Add(string surname, string name, string phoneNumber, string newContact)
        {
            addressBook.AddContact(surname, name, phoneNumber);
            ContactList.Items.Add(newContact);
        }

        public void Edit(string surname, string name, string phoneNumber, string editedContact)
        {
            int index = this.selection;

            addressBook.EditContact(index, surname, name, phoneNumber);

            ContactList.Items.RemoveAt(index);
            ContactList.Items.Insert(index, editedContact);
        }

        //Button Event Methods
        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = ContactList.SelectedIndex;
            if (!ContactList.Items.IsEmpty)
            {
                isSelected = true;
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            string path = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"S:\Fachinformatik\Auszubildende\MEhrenberg\Ausbildung\C# Aufgaben\Kleinprojekt Adressbuch(OOP) in WPF\Addressbooks\";
            if (openFileDialog.ShowDialog() == true) path = openFileDialog.FileName;

            if (path != "" && addressBook.FileExists(path))
            {
                addressBook.ImportFile(path);
                ContactList.Items.Clear();
                ShowInList();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        { 
            ContactWindow window = new ContactWindow(true);
            window.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected == true)
            {
                ContactWindow window = new ContactWindow(false);
                window.ShowDialog();     
            }
            isSelected = false;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected == true)
            {
                addressBook.RemoveContact(selection);
                ContactList.Items.RemoveAt(selection);
            }
            isSelected = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            addressBook.ClearBook();
            ContactList.Items.Clear();
            isSelected = false;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (!ContactList.Items.IsEmpty)
            {
                string path = "";

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.InitialDirectory = @"S:\Fachinformatik\Auszubildende\MEhrenberg\Ausbildung\C# Aufgaben\Kleinprojekt Adressbuch(OOP) in WPF\Addressbooks\";
                if (saveFileDialog.ShowDialog() == true) path = saveFileDialog.FileName;

                if (path != "") Export(path);
            }
            else
            {
                MessageBox.Show("Nothing to export.");
            }
        }
   
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContactList.Items.Clear();
            ShowInList();
        }
    }
} 


using System;
using AddressBook;

class Program
{
    static void Main()
    {
        AddressBookManager addressBook = new AddressBookManager();
        addressBook.LoadContacts();

        while (true)
        {
            Console.Clear();

            Console.WriteLine("\nAddress Book");
            Console.WriteLine("1. Add contact");
            Console.WriteLine("2. Show contacts");
            Console.WriteLine("3. Search contact");
            Console.WriteLine("4. Edit contact");
            Console.WriteLine("5. Delete contact");
            Console.WriteLine("6. Exit");

            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                addressBook.AddContact();
                Pause();
            }
            else if (choice == "2")
            {
                addressBook.ShowContacts();
                Pause();
            }
            else if (choice == "3")
            {
                addressBook.SearchContact();
                Pause();
            }
            else if (choice == "4")
            {
                addressBook.EditContact();
                Pause();
            }
            else if (choice == "5")
            {
                addressBook.DeleteContact();
                Pause();
            }
            else if (choice == "6")
            {
                Console.WriteLine("Exiting...");
                Pause();
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
                Pause();
            }
        }
    }

    static void Pause()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}
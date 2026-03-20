using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AddressBook
{
    internal class AddressBookManager
    {
        private List<Contact> contacts = new List<Contact>();
        private const string FilePath = "contacts.json";

        public void AddContact()
        {
            Contact contact = new Contact();

            contact.Name = ReadRequiredField("Name: ");
            contact.Phone = ReadRequiredField("Phone: ");
            contact.Email = ReadRequiredField("Email: ");

            contacts.Add(contact);
            SaveContacts();
            Console.WriteLine("Contact added!");
        }

        public void ShowContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                return;
            }

            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"\n{i + 1}. Name: {contacts[i].Name}");
                Console.WriteLine("Phone: " + contacts[i].Phone);
                Console.WriteLine("Email: " + contacts[i].Email);
            }
        }

        public void SearchContact()
        {
            Console.Write("Enter name to search: ");
            string? search = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(search))
            {
                Console.WriteLine("Search cannot be empty.");
                return;
            }

            bool found = false;

            for (int i = 0; i < contacts.Count; i++)
            {
                if ((contacts[i].Name ?? "").Contains(search, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\n{i + 1}. Name: {contacts[i].Name}");
                    Console.WriteLine("Phone: " + contacts[i].Phone);
                    Console.WriteLine("Email: " + contacts[i].Email);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No contacts found.");
            }
        }

        public void EditContact()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available to edit.");
                return;
            }

            ShowContacts();

            Console.Write("\nEnter contact number to edit: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int index))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            if (index < 1 || index > contacts.Count)
            {
                Console.WriteLine("Contact number out of range.");
                return;
            }

            Contact contact = contacts[index - 1];

            Console.Write("Enter new name: ");
            string? newName = Console.ReadLine();

            Console.Write("Enter new phone: ");
            string? newPhone = Console.ReadLine();

            Console.Write("Enter new email: ");
            string? newEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newName))
                contact.Name = newName;

            if (!string.IsNullOrWhiteSpace(newPhone))
                contact.Phone = newPhone;

            if (!string.IsNullOrWhiteSpace(newEmail))
                contact.Email = newEmail;

            SaveContacts();
            Console.WriteLine("Contact updated.");
        }

        public void DeleteContact()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available to delete.");
                return;
            }

            ShowContacts();

            Console.Write("Enter contact number to delete: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int index))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            if (index < 1 || index > contacts.Count)
            {
                Console.WriteLine("Contact number out of range.");
                return;
            }

            contacts.RemoveAt(index - 1);
            SaveContacts();
            Console.WriteLine("Contact removed.");
        }

        private string ReadRequiredField(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("This field cannot be empty.");
            }
        }
        public void SaveContacts()
        {
            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
        }
        public void LoadContacts()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
        }
    }
}
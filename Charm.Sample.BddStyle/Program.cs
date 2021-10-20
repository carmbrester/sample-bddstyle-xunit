using System;
using System.Threading.Tasks;

namespace Charm.Sample.BddStyle
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory("localhost", "5432", "contacts", "postgres", "postgres");
            var repository = new ContactsRepository(connectionFactory);

            Console.WriteLine("What's your username?");
            var username = Console.ReadLine();

            Console.Write("First Name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            var contact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            await repository.Insert(contact, username);
        }
    }
}

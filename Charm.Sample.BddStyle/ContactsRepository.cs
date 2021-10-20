using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Charm.Sample.BddStyle
{
    public class ContactsRepository
    {
        private IConnectionFactory ConnectionFactory { get; set; }

        public ContactsRepository(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public async Task Insert(Contact contact, string username)
        {
            var contactInsertSql = $@"
INSERT INTO public.contacts(
    first_name
    , last_name
    , email
    , when_created)
VALUES (@FirstName
    , @LastName
    , @Email
    , @WhenCreated)
RETURNING id;";

            using (var connection = ConnectionFactory.GetOpenConnection())
            {
                var id = await connection.ExecuteScalarAsync<int>(contactInsertSql,
                    new
                    {
                        contact.FirstName,
                        contact.LastName,
                        contact.Email,
                        WhenCreated = DateTime.Now
                    });

                contact._Id = id;

                await Insert(username, id, "Created Contact", connection);
            }
        }

        private async Task Insert(string username, int contactId, string changeDescription, System.Data.Common.DbConnection connection)
        {
            var sql = @$"
INSERT INTO public.contact_audits(
    contact_id
    , username
    , change_description
    , when_occurred)
VALUES (@ContactId
    , @Username
    , @ChangeDescription
    , @WhenOccurred);";

            await connection.ExecuteScalarAsync<int>(sql,
                new
                {
                    ContactId = contactId,
                    Username = username,
                    ChangeDescription = changeDescription,
                    WhenOccurred = DateTime.Now
                });
        }
    }
}

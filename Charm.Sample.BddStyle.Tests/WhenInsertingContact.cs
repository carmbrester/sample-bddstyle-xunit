using System;
using System.Threading.Tasks;
using AutoFixture;
using Xunit;

namespace Charm.Sample.BddStyle.Tests
{
    [Collection("Database collection")]
    public class WhenInsertingContact : IAsyncLifetime
    {
        private DatabaseFixture _fixture;
        private IFixture _autofixture;
        private IConnectionFactory _connectionFactory;
        private ContactsRepository _contactsRepository;

        private Contact _contact;

        private string _username;

        private int _initialContactRowCount;
        private int _initialContactAduitRowCount;

        private int _contactId;

        public WhenInsertingContact(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _autofixture = new Fixture();
            _contactsRepository = new ContactsRepository(_fixture);

            _contact = new Contact
            {
                FirstName = _autofixture.Create<string>(),
                LastName = _autofixture.Create<string>(),
                Email = _autofixture.Create<string>(),
            };
            _username = _autofixture.Create<string>();

            _initialContactRowCount = RepositoryTestsHelper.GetTableRowCount(_fixture, "contacts");
            _initialContactAduitRowCount = RepositoryTestsHelper.GetTableRowCount(_fixture, "contact_audits");
            await _contactsRepository.Insert(_contact, _username);
        }

        [Fact]
        public void ShouldIncreaseContactsRowCount()
        {
            var afterInsertContactRowCount = RepositoryTestsHelper.GetTableRowCount(_fixture, "contacts");
            Assert.Equal(_initialContactRowCount + 1, afterInsertContactRowCount);
        }

        [Fact]
        public void ShouldIncreaseContactAuditsRowCount()
        {
            var afterInsertContactRowCount = RepositoryTestsHelper.GetTableRowCount(_fixture, "contact_audits");
            Assert.Equal(_initialContactRowCount + 1, afterInsertContactRowCount);
        }

        [Fact]
        public void ShouldSetIdOnContact()
        {
            Assert.NotEqual(0, _contact.Id);
        }
    }
}

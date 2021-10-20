using System.Data.Common;

namespace Charm.Sample.BddStyle
{
    public interface IConnectionFactory
    {
        DbConnection GetOpenConnection();
    }
}

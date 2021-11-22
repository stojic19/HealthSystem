using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pharmacy.EfStructures;
using Pharmacy.Repositories.Base;
using Xunit;

namespace PharmacyUnitTests.Base
{
    [Collection("UnitTests")]
    public abstract class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;

        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }

        protected IUnitOfWork UoW => _fixture.UoW;
        protected AppDbContext Context => _fixture.Context;

        public static void GenericRemoveSet<T>(DbSet<T> set) where T : class
        {
            foreach (var item in set)
            {
                set.Remove(item);
            }
        }

        protected void ClearDbContext()
        {
            var removeMethod = System.Reflection.MethodBase
                .GetCurrentMethod()
                .DeclaringType
                .GetMethod(nameof(GenericRemoveSet));

            var properties = Context
                .GetType()
                .GetProperties()
                .Where(x => x.PropertyType.IsGenericType &&
                            x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var property in properties)
            {
                var genericArgument = property.PropertyType.GetGenericArguments();
                var typedRemove = removeMethod.MakeGenericMethod(genericArgument);
                typedRemove.Invoke(null, new object[] { property.GetValue(Context) });
            }

            Context.SaveChanges();
        }
    }
}

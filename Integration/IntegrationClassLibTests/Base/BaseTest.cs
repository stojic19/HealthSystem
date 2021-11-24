using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Integration.EfStructures;
using Integration.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationClassLibTests.Base
{
    [Collection("Tests")]
    public abstract class BaseTest : IClassFixture<BaseFixture>
    {
        private readonly BaseFixture _fixture;
        protected BaseTest(BaseFixture fixture)
        {
            _fixture = fixture;
        }
        protected IUnitOfWork UoW => _fixture.UoW;
        protected AppDbContext Context => _fixture.Context;
        public CookieContainer CookieContainer => _fixture.CookieContainer;
        public string BaseUrl => "https://localhost:44303/";

        public void AddCookie(string name, string value, string domain)
        {
            CookieContainer.Add(new Cookie(name, value) { Domain = domain });
        }

        public StringContent GetContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

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

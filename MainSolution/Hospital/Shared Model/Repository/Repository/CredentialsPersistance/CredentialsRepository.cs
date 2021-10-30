using Model;
using System.Collections.Generic;

namespace Repository.CredentialsPersistance
{
    public class CredentialsRepository : ICredentialsRepository
    {
        public void Create(Credentials newValue)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateIfUnique(Credentials newValue)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Credentials GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public List<Credentials> GetValues()
        {
            throw new System.NotImplementedException();
        }

        public void Save(List<Credentials> values)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Credentials newValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
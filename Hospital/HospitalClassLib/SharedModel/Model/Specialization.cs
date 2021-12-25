using Microsoft.Extensions.Options;

namespace Hospital.SharedModel.Model
{
    public class Specialization
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Specialization()
        {
            
        }
        public Specialization(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Validate();
        }

        private void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}

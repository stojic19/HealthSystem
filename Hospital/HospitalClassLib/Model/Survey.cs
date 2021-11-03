using System;
using System.Collections.Generic;

namespace Hospital.Model
{
    public class Survey
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}

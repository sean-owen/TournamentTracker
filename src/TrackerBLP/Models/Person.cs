using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace TrackerBLP.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return (FullName != string.Empty) ? FullName : base.ToString();
        }
    }
}

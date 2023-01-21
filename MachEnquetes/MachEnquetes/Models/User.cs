using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachEnquetes.Models
{
    public class User
    {
        public string Id
        {
            get => default;
            set
            {
            }
        }

        public string Name
        {
            get => default;
            set
            {
            }
        }

        public string Email
        {
            get => default;
            set
            {
            }
        }

        public string Password
        {
            get => default;
            set
            {
            }
        }

        public DateTime DateBirth
        {
            get => default;
            set
            {
            }
        }

        public List<Survey> Surveys
        {
            get => default;
            set
            {
            }
        }

        public DateTime? LastModifiedDate { get; set; }
    }
}
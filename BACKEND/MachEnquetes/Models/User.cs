﻿using MachEnquetes.Application;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachEnquetes.Models
{
    /// <summary>
    /// A user.
    /// </summary>
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerExclude]
        public int Id
        {
            get;
            set;
        }

        /// <example>Wellington Macena</example>
        public string FullName
        {
            get;
            set
            ;
        }

        /// <example>wellington.macena.23@gmail.com</example>
        public string Email
        {
            get;
            set;
        }

        /// <example>teste</example>
        public string Password
        {
            get;
            set;
        }

        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? DateBirth
        {
            get;
            set;
        }

        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? LastModifiedDate { get; set; }
        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? CreatedDate { get; set; }

        public User(string fullName, string email, string password) : this()
        {
            FullName = fullName;
            Email = email;
            Password = password;
            DateBirth = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
            CreatedDate = DateTime.UtcNow;
        }

        public User()
        {
         //   Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
        }

        public User(string id) : this()
        {
           // Id = id;
        }
    }
}
using MachEnquetes.Application;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace MachEnquetes.Models
{
    /// <summary>
    /// A user.
    /// </summary>
     [Table("User")]
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
      
        public User(string fullName, string email, string password, string birthDate) : this()
        {
            FullName = fullName;
            Email = email;
            Password = password;
            DateBirth = DateTime.ParseExact(birthDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;            
        }

        public User(string fullName, string email, string password) : this()
        {
            FullName = fullName;
            Email = email;
            Password = password;
            DateBirth = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
        }

        public User()
        {
         //   Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
        }


        public override string? ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{{Id: {Id}, ");
            sb.AppendLine($"FullName: {FullName}, ");
            sb.AppendLine($"Email: {Email}, ");
            sb.AppendLine($"Password: {Password}, ");
            sb.AppendLine($"DateBirth: {DateBirth}, ");
            sb.AppendLine($"CreatedDate: {CreatedDate}, ");
            sb.AppendLine($"LastModifiedDate: {LastModifiedDate}}}");

            return sb.ToString();
        }
    }
}
using MachEnquetes.Application;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachEnquetes.Models
{
    [Table("Survey")]
    public class Survey
    {
        [SwaggerExclude]
        public string Id
        {
            get => default;
            set
            {
            }
        }

        /// <example>What is your favorite color?</example>
        public string Title
        {
            get => default;
            set
            {
            }
        }
  
        /// <example>No</example>
        public bool CanUnregistredVote
        {
            get => default;
            set
            {
            }
        }

        /// <example>No</example>
        public bool CanUserUpdateVote
        {
            get => default;
            set
            {
            }
        }

        /// <example>2</example>
        public int OptionsSelectedCount
        {
            get => default;
            set
            {
            }
        }

        public int FKCreatorUser
        {
            get => default;
            set
            {
            }
        }

        public int VotesCount
        {
            get => default;
            set
            {
            }
        }

        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? LastModifiedDate { get; set; }
        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? CreatedDate { get; set; }
        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime FinishDate
        {
            get => default;
            set
            {
            }
        }


        public Survey()
        {

        }

        public Survey(string id) : this()
        {
            Id = id;
        }
    }
}
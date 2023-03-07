using System.ComponentModel.DataAnnotations.Schema;

namespace MachEnquetes.Models
{
    [Table("Vote")]
    public class Vote
    {
        public int Id
        {
            get => default;
            set
            {
            }
        }

        public int FKVoter
        {
            get => default;
            set
            {
            }
        }

        public int FKVoteOption
        {
            get => default;
            set
            {
            }
        }

        public int FKSurvey
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
    }
}
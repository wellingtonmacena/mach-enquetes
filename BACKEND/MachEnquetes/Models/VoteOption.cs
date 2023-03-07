using System.ComponentModel.DataAnnotations.Schema;

namespace MachEnquetes.Models
{
    [Table("VoteOption")]
    public class VoteOption
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public int FKSurvey
        {
            get => default;
            set
            {
            }
        }

        public DateTime CreatedDate
        {
            get => default;
            set
            {
            }
        }

        public DateTime LastModifiedDate
        {
            get => default;
            set
            {
            }
        }
    }
}
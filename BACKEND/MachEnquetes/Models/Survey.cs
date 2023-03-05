using MachEnquetes.Application;

namespace MachEnquetes.Models
{
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
        /// <example>["red", "blue", "yellow"]</example>
        public List<VoteOption> VoteOptions
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

        public List<Vote> Votes
        {
            get => default;
            set
            {
            }
        }
        /// <example>2</example>
        public int QuantityOfOptionsCanBeSelected
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
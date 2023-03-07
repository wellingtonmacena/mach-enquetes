using MachEnquetes.Application;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace MachEnquetes.Models
{
    [Table("Survey")]
    public class Survey
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerExclude]
        public int Id { get; set; }

        /// <example>What is your favorite color?</example>
        public string Title { get; set; }

        /// <example>No</example>
        public bool CanUnregistredVote { get; set; }

        /// <example>No</example>
        public bool CanUserUpdateVote { get; set; }

        /// <example>2</example>
        public int OptionsSelectedCount { get; set; }

        public int FKCreatorUser { get; set; }

        public int VotesCount { get; set; }

        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? LastModifiedDate { get; set; }
        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime? CreatedDate { get; set; }
        /// <example>"2017-09-08T19:01:55.714942+03:00"</example>
        public DateTime FinishDate { get; set; }

        public Survey(int id, string title, bool canUnregistredVote, bool canUserUpdateVote, int optionsSelectedCount, string finishDate) : this(id)
        {
            Title = title;
            CanUnregistredVote = canUnregistredVote;
            CanUserUpdateVote = canUserUpdateVote;
            OptionsSelectedCount = optionsSelectedCount;
            FinishDate = DateTime.ParseExact(finishDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public Survey(int id, string title, bool canUnregistredVote, bool canUserUpdateVote,
            int optionsSelectedCount, int fKCreatorUser, int votesCount,
            DateTime? lastModifiedDate, DateTime? createdDate, DateTime finishDate) : this(id)
        {
            Title = title;
            CanUnregistredVote = canUnregistredVote;
            CanUserUpdateVote = canUserUpdateVote;
            OptionsSelectedCount = optionsSelectedCount;
            FKCreatorUser = fKCreatorUser;
            VotesCount = votesCount;
            LastModifiedDate = lastModifiedDate;
            CreatedDate = createdDate;
            FinishDate = finishDate;
        }

        public Survey()
        {
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
        }

        public Survey(int id) : this()
        {
            Id = id;
        }

        public override string? ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{{Id: {Id}, ");
            sb.AppendLine($"Title: {Title}, ");
            sb.AppendLine($"CanUnregistredVote: {CanUnregistredVote}, ");
            sb.AppendLine($"CanUserUpdateVote: {CanUserUpdateVote}, ");
            sb.AppendLine($"OptionsSelectedCount: {OptionsSelectedCount}, ");
            sb.AppendLine($"FKCreatorUser: {FKCreatorUser}, ");
            sb.AppendLine($"FinishDate: {FinishDate}, ");
            sb.AppendLine($"CreatedDate: {CreatedDate}, ");
            sb.AppendLine($"LastModifiedDate: {LastModifiedDate}}}");

            return sb.ToString();
        }
    }
}
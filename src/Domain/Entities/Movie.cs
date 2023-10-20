using Domain.Common;

namespace Domain.Entities
{
    public class Movie : AuditableEntity
    {

        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string Genres { get; set; } = String.Empty;

        public int Year { get; set; } = 0;

        public string Country { get; set; } = String.Empty;

        public string Director { get; set; } = String.Empty;

        public string Starring { get; set; } = String.Empty;

        public string Original_Language { get; set; } = String.Empty;


    }
}

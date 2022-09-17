namespace FootballClubWeb.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; }

        public News()
        {

        }
    }
}

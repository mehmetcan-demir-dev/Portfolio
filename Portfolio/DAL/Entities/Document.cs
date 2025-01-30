namespace Portfolio.DAL.Entities
{
    public class Document
    {
        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}

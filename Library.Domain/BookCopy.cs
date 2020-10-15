namespace Library.Domain
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int DetailsId { get; set; }
        public BookDetails Details { get; set; }
    }
}
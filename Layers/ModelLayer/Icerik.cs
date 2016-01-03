
namespace ModelLayer
{
    
    public class Icerik
    {
        public int id { get; set; }
        public string sayfaBaslik {get; set;}
        public string title {get; set;}
        public string description {get; set;}
        public string keywords {get; set;}
        public string icerik { get; set; }

        public Icerik()
        { 
        
        }

        public Icerik(string title, string description, string keywords, string icerik)
        {
            this.title = title;
            this.description = description;
            this.keywords = keywords;
            this.icerik = icerik;

        }
    }
}

namespace ATframework3demo.TestEntities
{
    public class Post
    {
        public Post(string title, string description, string type)
        {
            Title = title;
            Description = description;
            Type = type;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
}

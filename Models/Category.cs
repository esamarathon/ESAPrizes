namespace ESAPrizes.Models
{
    public class Category
    {
        /// Gets or sets the name of the category
        public string Name { get; set; }

        /// Gets or sets a shorter version of the name.
        public string ShortName { get; set; }

        /// Gets or sets the category sort order
        public int Order { get; set; }

        public Category(string name) : this(name, name, 0)
        {
        }

        public Category(string name, string shortName, int order = 0)
        {
            Name = name;
            ShortName = shortName;
            Order = order;
        }
    }
}
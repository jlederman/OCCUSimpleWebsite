using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<CrudItem> CrudItems { get; set; }
    public virtual DbSet<StatusListModel> StatusList { get; set; }

    public static void Seed37(ApplicationDbContext context)
    {
        if (context == null) 
            throw new ArgumentNullException("It appears we do not have any context for the database");

        if (!context.CrudItems.Any())
        {
            var seed37CrudItems = new List<CrudItem> { };
            Random random = new Random();
            for (int i = 0; i < 37; i++)
            {
                seed37CrudItems.Add(new CrudItem
                {
                    Name = $"Name {i}",
                    Description = $"Description {i}",
                    Status = GetRandomEnumValue<CrudItemStatus>(random).ToString(),
                    LastEdit = DateTime.Now
                });
            }

            context.CrudItems.AddRange(seed37CrudItems);
            context.SaveChanges();
        }
    }

    public static void StatusListSeed(ApplicationDbContext context)
    {
        if (context == null)
            throw new ArgumentNullException("It appears we do not have any context for the database");
        
        if (!context.StatusList.Any())
        {
            var seed37StatusList = new List<StatusListModel> { };
            Random random = new Random();
            for (int i = 0; i < 37; i++)
            {
                seed37StatusList.Add(new StatusListModel
                {
                    Name = $"Name {i}",
                    Status = GetRandomEnumValue<Status>(random).ToString(),
                });
            }

            context.StatusList.AddRange(seed37StatusList);
            context.SaveChanges();
        }

    }

    // We want to seed 37 rows with randomized status properties 
    // Can't think of a different way to do this that's more readable. This looks ugly. The gist
    // here is to use a generic method, which works with any enum, to retrieve all specified 
    // enum types via CrudItemStatus in the CrudItemModel then pick one for each row at random
    private static T GetRandomEnumValue<T>(Random random) where T : Enum
    {
        var values = Enum.GetValues(typeof(T)); 
        return (T)values.GetValue(random.Next(values.Length)); 
    }

}
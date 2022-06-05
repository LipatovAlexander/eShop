namespace Core.Entities;
public class Customer : BaseEntity
{
    public int IdentityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public File Avatar { get; set; }
}

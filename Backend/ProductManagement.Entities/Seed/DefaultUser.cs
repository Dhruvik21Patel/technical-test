using ProductManagement.Entities.DataModels;

namespace ProductManagement.Entities.Seed
{
    public class DefaultUser
    {
        public static List<User> IdentityList()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "system_admin@gmail.com",
                    Password = "Admin@123",
                    CreatedBy = 1,
                    CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                    UpdatedBy = 1,
                    UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                }
            };
        }
    }
}
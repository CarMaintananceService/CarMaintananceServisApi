
namespace Business.Shared.Security.Users.Dtos
{
    public class SimpleUserDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? UserName { get; set; }
        public int EmployeeId { get; set; }
        public string? Name { get; set; }

    }
}

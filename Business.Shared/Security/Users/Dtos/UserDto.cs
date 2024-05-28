
using Core.Shared;


namespace Business.Shared.Security.Users.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public string? Code { get; set; }
        public string? UserName { get; set; }
        public int CookieTimeout { get; set; }
        public int Source { get; set; }
        public string? EMail { get; set; }
        public string? Name { get; set; }
        public string? NameSecond { get; set; }
        public string? Surname { get; set; }
        public string? SurnameSecond { get; set; }
    }
}

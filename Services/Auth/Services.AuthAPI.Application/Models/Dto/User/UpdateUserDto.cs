namespace Services.AuthAPI.Application.Models.Dto.User
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}

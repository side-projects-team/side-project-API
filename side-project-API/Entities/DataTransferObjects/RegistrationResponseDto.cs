namespace side_project_API.Entities.DataTransferObjects
{
    public class RegistrationResponseDto
    {
        public bool isSuccessfulRegistration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
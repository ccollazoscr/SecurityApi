namespace Security.Infraestructure.Entity
{
    public class SecurityUserEntity
    {
        public long SecurityUserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}

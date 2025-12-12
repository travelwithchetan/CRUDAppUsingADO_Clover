namespace CRUDAppUsingADO.Logging
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public void SaveUser(string message)
        {
            _logger.LogInformation("User Saved Successfully");
        }
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

    }
}

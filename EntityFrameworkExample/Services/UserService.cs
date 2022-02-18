namespace EntityFrameworkExample.Services
{
    public interface IUserService
    {
        string GetUserId();
    }

    public class UserService: IUserService
    {
        public string GetUserId()
        {
            return "The user";
        }
    }
}

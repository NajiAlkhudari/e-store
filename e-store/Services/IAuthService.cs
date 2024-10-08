using e_store.Models;

namespace e_store.Models
{
    public interface IAuthService
    {
        Task<AuthModel> RejesterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(RegisterModel model);
    }
}

using Application.Abstractions;

namespace API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
        public string? UserId => contextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
    }
}

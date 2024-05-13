using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Repositories
{
	public interface ITokenRepository
	{
		string CreateJWTToken(IdentityUser user, List<string> roles);
	}
}

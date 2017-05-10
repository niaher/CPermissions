namespace CPermissions
{
	using System.Collections.Generic;

	public interface IRoleChecker<in TUser, out TRole>
	{
		/// <summary>
		/// Gets all roles that user has in any context across the system.
		/// </summary>
		/// <param name="user">User for whom to check the roles.</param>
		/// <returns>List of roles.</returns>
		IEnumerable<TRole> GetRoles(TUser user);
	}
}
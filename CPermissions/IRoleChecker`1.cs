namespace CPermissions
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a class which can retrieve user's roles for a specific object.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TContext"></typeparam>
	public interface IRoleChecker<in TUser, out TRole, in TContext>
	{
		/// <summary>
		/// Gets all roles that user has in any context across the system.
		/// </summary>
		/// <param name="user">User for whom to check the roles.</param>
		/// <param name="context">Context entity against which to check role membership.</param>
		/// <returns>List of roles.</returns>
		IEnumerable<TRole> GetRoles(TUser user, TContext context);
	}
}
namespace CPermissions
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents an object which is able to check whether a specific user
	/// is able to perform specific actions.
	/// </summary>
	/// <typeparam name="TUserAction"></typeparam>
	/// <typeparam name="TUser"></typeparam>
	public interface IPermissionManager<TUserAction, in TUser>
	{
		/// <summary>
		/// Checks if user has permission to perform a certain action.
		/// </summary>
		/// <param name="userAction">Action which is to be performed.</param>
		/// <param name="user">User for whom to check the permissions.</param>
		/// <returns>True if the user role(s) give(s) him the permission to perform an action.</returns>
		bool CanDo(TUserAction userAction, TUser user);

		/// <summary>
		/// Makes sure that the user has permission to perform a certain action.
		/// If user doesn't have the required permission, then an exception is thrown.
		/// </summary>
		/// <param name="userAction">Action which is to be performed.</param>
		/// <param name="user">User for whom to check the permissions.</param>
		void EnforceCanDo(TUserAction userAction, TUser user);

		/// <summary>
		/// Gets a list of allowed user actions for the specified user.
		/// </summary>
		/// <param name="user">User for whom to check the permissions.</param>
		/// <returns>List of allowed user actions.</returns>
		IEnumerable<TUserAction> GetAllowedUserActions(TUser user);
	}
}
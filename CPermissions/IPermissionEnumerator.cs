namespace CPermissions
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents an object which can retrieve allowed actions for the given user.
	/// </summary>
	/// <typeparam name="TUserAction"></typeparam>
	/// <typeparam name="TActor"></typeparam>
	public interface IPermissionEnumerator<out TUserAction, in TActor>
	{
		/// <summary>
		/// Gets all allowed actions for the given user.
		/// </summary>
		/// <param name="actor"></param>
		/// <returns></returns>
		IEnumerable<TUserAction> GetAllowedUserActions(TActor actor);
	}
}
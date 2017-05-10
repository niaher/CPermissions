namespace CPermissions
{
	using System.Collections.Generic;

	public interface IPermissionEnumerator<out TUserAction, in TActor>
	{
		IEnumerable<TUserAction> GetAllowedUserActions(TActor actor);
	}
}
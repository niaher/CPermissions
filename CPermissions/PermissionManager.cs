namespace CPermissions
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class PermissionManager<TUser, TRole> : 
		IPermissionManager<UserAction, TUser>,
		IPermissionEnumerator<UserAction, TRole>
	{
		protected PermissionManager(IRoleChecker<TUser, TRole> roleChecker)
		{
			this.RoleChecker = roleChecker;
		}

		protected IRoleChecker<TUser, TRole> RoleChecker { get; private set; }

		public bool CanDo(UserAction userAction, TUser user)
		{
			var roles = this.RoleChecker.GetRoles(user);
			return roles.Any(r => this.RoleCanDo(userAction, r));
		}

		public void EnforceCanDo(UserAction userAction, TUser user)
		{
			if (!this.CanDo(userAction, user))
			{
				throw new PermissionException<TUser>(userAction, user);
			}
		}
		
		public IEnumerable<UserAction> GetAllowedUserActions(TUser user)
		{
			var allowedActions = new List<UserAction>();

			var roles = this.RoleChecker.GetRoles(user);
			foreach (var userRole in roles)
			{
				allowedActions.AddRange(this.GetAllowedUserActions(userRole));
			}

			return allowedActions;
		}

		public abstract IEnumerable<UserAction> GetAllowedUserActions(TRole role);

		private bool RoleCanDo(UserAction userAction, TRole r)
		{
			return this.GetAllowedUserActions(r).Any(a => a == userAction);
		}
	}
}
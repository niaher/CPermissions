namespace CPermissions
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class PermissionManager<TUser, TRole, TContext> :
		IPermissionManager<UserAction<TContext>, TUser, TContext>,
		IPermissionEnumerator<UserAction<TContext>, TRole>
	{
		protected IRoleChecker<TUser, TRole, TContext> RoleChecker { get; private set; }

		protected PermissionManager(IRoleChecker<TUser, TRole, TContext> roleChecker)
		{
			this.RoleChecker = roleChecker;
		}

		public bool CanDo(UserAction<TContext> userAction, TUser user, TContext context)
		{
			var roles = this.RoleChecker.GetRoles(user, context);
			return roles.Any(r => this.RoleCanDo(userAction, r));
		}

		public void EnforceCanDo(UserAction<TContext> userAction, TUser user, TContext context)
		{
			if (!this.CanDo(userAction, user, context))
			{
				throw new PermissionException<TUser, TContext>(userAction, user);
			}
		}

		public IEnumerable<UserAction<TContext>> GetAllowedUserActions(TUser user, TContext context)
		{
			var allowedActions = new List<UserAction<TContext>>();

			var roles = this.RoleChecker.GetRoles(user, context);
			foreach (var userRole in roles)
			{
				allowedActions.AddRange(this.GetAllowedUserActions(userRole));
			}

			return allowedActions;
		}

		public abstract IEnumerable<UserAction<TContext>> GetAllowedUserActions(TRole role);

		private bool RoleCanDo(UserAction<TContext> userAction, TRole r)
		{
			return this.GetAllowedUserActions(r).Any(a => a == userAction);
		}
	}
}
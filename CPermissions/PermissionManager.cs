namespace CPermissions
{
	using System.Collections.Generic;
	using System.Linq;

	/// <inheritdoc cref="IPermissionManager{TUserAction,TUser}" />
	public abstract class PermissionManager<TUser, TRole> :
		IPermissionManager<UserAction, TUser>,
		IPermissionEnumerator<UserAction, TRole>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionManager{TUser,TRole}"/> class.
		/// </summary>
		/// <param name="roleChecker"></param>
		protected PermissionManager(IRoleChecker<TUser, TRole> roleChecker)
		{
			this.RoleChecker = roleChecker;
		}

		/// <summary>
		/// Role checker which can retrieve roles of type <typeparamref name="TRole"/>
		/// for user of type <typeparamref name="TUser"/>.
		/// </summary>
		protected IRoleChecker<TUser, TRole> RoleChecker { get; }

		/// <inheritdoc />
		public abstract IEnumerable<UserAction> GetAllowedUserActions(TRole role);

		/// <inheritdoc />
		public bool CanDo(UserAction userAction, TUser user)
		{
			var roles = this.RoleChecker.GetRoles(user);
			return roles.Any(r => this.RoleCanDo(userAction, r));
		}

		/// <inheritdoc />
		public void EnforceCanDo(UserAction userAction, TUser user)
		{
			if (!this.CanDo(userAction, user))
			{
				throw new PermissionException<TUser>(userAction, user);
			}
		}

		/// <inheritdoc />
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

		private bool RoleCanDo(UserAction userAction, TRole r)
		{
			return this.GetAllowedUserActions(r).Any(a => a == userAction);
		}
	}
}
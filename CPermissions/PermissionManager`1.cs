namespace CPermissions
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Represents a class which can check if user has permissions to perform
	/// a specific action on a specific object.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TContext"></typeparam>
	public abstract class PermissionManager<TUser, TRole, TContext> :
		IPermissionManager<UserAction<TContext>, TUser, TContext>,
		IPermissionEnumerator<UserAction<TContext>, TRole>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionManager{TUser,TRole,TContext}"/> class.
		/// </summary>
		/// <param name="roleChecker">Instance of <see cref="IRoleChecker{TUser,TRole,TContext}"/>.</param>
		protected PermissionManager(IRoleChecker<TUser, TRole, TContext> roleChecker)
		{
			this.RoleChecker = roleChecker;
		}

		/// <summary>
		/// Role checker which can retrieve roles of type <typeparamref name="TRole"/>
		/// for user of type <typeparamref name="TUser"/> on objects of type <typeparamref name="TContext"/>.
		/// </summary>
		protected IRoleChecker<TUser, TRole, TContext> RoleChecker { get; }

		/// <inheritdoc />
		public abstract IEnumerable<UserAction<TContext>> GetAllowedUserActions(TRole role);

		/// <inheritdoc />
		public bool CanDo(UserAction<TContext> userAction, TUser user, TContext context)
		{
			var roles = this.RoleChecker.GetRoles(user, context);
			return roles.Any(r => this.RoleCanDo(userAction, r));
		}

		/// <inheritdoc />
		public void EnforceCanDo(UserAction<TContext> userAction, TUser user, TContext context)
		{
			if (!this.CanDo(userAction, user, context))
			{
				throw new PermissionException<TUser, TContext>(userAction, user);
			}
		}

		/// <inheritdoc />
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

		private bool RoleCanDo(UserAction<TContext> userAction, TRole r)
		{
			return this.GetAllowedUserActions(r).Any(a => a == userAction);
		}
	}
}
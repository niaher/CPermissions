namespace CPermissions
{
	using System;

	public class PermissionException<TUser, TContext> : Exception
	{
		public PermissionException(UserAction<TContext> action, TUser user)
		{
			this.User = user;
			this.UserAction = action;
		}

		public UserAction<TContext> UserAction { get; set; }
		public TUser User { get; set; }

		public override string Message
		{
			get
			{
				return string.Format("Permission '{0}' denied for user {1}.", this.UserAction.Name, this.User);
			}
		}
	}
}
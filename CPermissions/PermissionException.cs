namespace CPermissions
{
	using System;

	/// <summary>
	/// Thrown when permission check fails, meaning user has no permission for a specific action.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	public class PermissionException<TUser> : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionException{TUser}"/> class.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="user"></param>
		public PermissionException(UserAction action, TUser user)
		{
			this.User = user;
			this.UserAction = action;
		}

		/// <summary>
		/// Gets action that user attempted to perform.
		/// </summary>
		public UserAction UserAction { get; set; }

		/// <summary>
		/// Gets user who attempted to perform the action.
		/// </summary>
		public TUser User { get; set; }

		/// <summary>
		/// Gets friendly message describing the error.
		/// </summary>
		public override string Message => string.Format("Permission '{0}' denied for user {1}.", this.UserAction.Name, this.User);
	}
}
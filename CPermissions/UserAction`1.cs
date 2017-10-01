namespace CPermissions
{
	using System;

	/// <summary>
	/// Represents an action which can be performed by a user on a specific
	/// type of object.
	/// </summary>
	/// <typeparam name="TContext">Type of object on which this action can be performed.</typeparam>
	public class UserAction<TContext> : UserAction
	{
		/// <inheritdoc />
		public UserAction(string name) : base(name)
		{
			this.ContextType = typeof(TContext);
		}

		/// <summary>
		/// Gets or sets the type for which the permission can be defined.
		/// </summary>
		public Type ContextType { get; }
	}
}
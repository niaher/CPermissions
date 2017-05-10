namespace CPermissions
{
	using System;

	public class UserAction<TContext>
	{
		public UserAction(string name)
		{
			this.Name = name;
			this.ContextType = typeof(TContext);
		}

		public string Name { get; protected set; }

		/// <summary>
		/// Gets or sets the type for which the permission can be defined.
		/// </summary>
		public Type ContextType { get; private set; }
	}
}
namespace CPermissions
{
	/// <summary>
	/// Represents an action which can be performed by a user.
	/// </summary>
	public class UserAction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserAction"/> class.
		/// </summary>
		/// <param name="name"></param>
		public UserAction(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Gets friendly name representing the action.
		/// </summary>
		public string Name { get; set; }
	}
}
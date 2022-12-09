namespace LazySloth
{
	/// <summary>
	/// Exception to throw if a member could not be found via reflection
	/// </summary>
	public class MemberNotFoundException : System.Exception
	{
		/// <inheritdoc />
		public MemberNotFoundException() : base()
		{
			
		}
		
		/// <inheritdoc />
		public MemberNotFoundException(string inMessage) : base(inMessage)
		{
			
		}

		/// <inheritdoc />
		public MemberNotFoundException(string inMessage, System.Exception inInner) : base(inMessage, inInner)
		{
			
		}
	}
}

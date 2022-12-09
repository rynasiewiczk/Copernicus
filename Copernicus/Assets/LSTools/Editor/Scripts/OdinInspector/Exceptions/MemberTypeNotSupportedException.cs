namespace LazySloth
{
	/// <summary>
	/// Exception to throw if the type of a member is not supported for a particular use case
	/// </summary>
	public class MemberTypeNotSupportedException : System.Exception
	{
		/// <inheritdoc />
		public MemberTypeNotSupportedException() : base()
		{
			
		}
		
		/// <inheritdoc />
		public MemberTypeNotSupportedException(string inMessage) : base(inMessage)
		{
			
		}

		/// <inheritdoc />
		public MemberTypeNotSupportedException(string inMessage, System.Exception inInner) : base(inMessage, inInner)
		{
			
		}
	}
}

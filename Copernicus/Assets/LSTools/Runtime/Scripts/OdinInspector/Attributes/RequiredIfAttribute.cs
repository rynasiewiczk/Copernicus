using Sirenix.OdinInspector;

namespace LazySloth
{
    /// <summary>
    /// A conditional version of a <see cref="Sirenix.OdinInspector.RequiredAttribute"/>
    /// </summary>
    [System.AttributeUsage
    (
        System.AttributeTargets.Field 
        | System.AttributeTargets.Property 
        | System.AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true
    )]
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public class RequiredIfAttribute : System.Attribute
    {
        public string MemberName;
        public object Value;
        
        public RequiredAttribute RequiredAttribute; 
        
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="inMemberName">
        /// The name of the member to validate. Supported types are: field, property, or 0-arg method
        /// </param>
        /// <param name="inValue">The value the member needs to have or return in order to run validation</param>
        public RequiredIfAttribute(string inMemberName, object inValue)
        {
            MemberName = inMemberName;
            Value = inValue;
            RequiredAttribute = new RequiredAttribute();
        }

        /// <summary>
        /// Provides the given error message if validation fails
        /// </summary>
        /// <param name="inMemberName">
        /// The name of the member to validate. Supported types are: field, property, or 0-arg method
        /// </param>
        /// <param name="inValue">The value the member needs to have or return in order to run validation</param>
        /// <param name="inErrorMessage">The error message to display if validation fails</param>
        public RequiredIfAttribute(string inMemberName, object inValue, string inErrorMessage)
        {
            MemberName = inMemberName;
            Value = inValue;
            RequiredAttribute = new RequiredAttribute(inErrorMessage);
        }
        
        /// <summary>
        /// Provides the default error message with the given message type if validation fails
        /// </summary>
        /// <param name="inMemberName">
        /// The name of the member to validate. Supported types are: field, property, or 0-arg method
        /// </param>
        /// <param name="inValue">The value the member needs to have or return in order to run validation</param>
        /// <param name="inInfoMessageType">The message type to use if validation fails</param>
        public RequiredIfAttribute(string inMemberName, object inValue, InfoMessageType inInfoMessageType)
        {
            MemberName = inMemberName;
            Value = inValue;
            RequiredAttribute = new RequiredAttribute(inInfoMessageType);
        }
        
        /// <summary>
        /// Provides the given error message with the given message type if validation fails
        /// </summary>
        /// <param name="inMemberName">
        /// The name of the member to validate. Supported types are: field, property, or 0-arg method
        /// </param>
        /// <param name="inValue">The value the member needs to have or return in order to run validation</param>
        /// <param name="inErrorMessage">The error message to display if validation fails</param>
        /// /// <param name="inInfoMessageType">The message type to use if validation fails</param>
        public RequiredIfAttribute(string inMemberName, object inValue, string inErrorMessage, InfoMessageType inInfoMessageType)
        {
            MemberName = inMemberName;
            Value = inValue;
            RequiredAttribute = new RequiredAttribute(inErrorMessage, inInfoMessageType);
        }
    }
}

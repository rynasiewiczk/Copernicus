using System.Reflection;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(LazySloth.Editor.RequiredIfValidator))]

namespace LazySloth.Editor
{
    using Sirenix.OdinInspector.Editor.ValueResolvers;

    /// <summary>
    /// Attribute validator for <see cref="RequiredIfAttribute"/>
    /// </summary>
    public class RequiredIfValidator : AttributeValidator<RequiredIfAttribute>
    {
        /** Helper class to get names for the members of objects */
        private ValueResolver<string> _StringMemberHelper;

        /// <inheritdoc />
        public override void Initialize(MemberInfo member, System.Type memberValueType)
        {
            base.Initialize(member, memberValueType);
            
            if (Attribute.RequiredAttribute.ErrorMessage != null)
            {
                _StringMemberHelper = ValueResolver.Get<string>(Property, Attribute.RequiredAttribute.ErrorMessage);
            }
        }

        /// <inheritdoc />
        protected override void Validate(object inParentInstance, object inMemberValue, MemberInfo inMemberInfo, ValidationResult inResult)
        {
            if (ParentHasRequiredValue(inParentInstance) == true)
            {
                if (OdinValidateRequiredObject(inMemberValue) == false)
                {
                    OdinValidationFailed(inParentInstance, inMemberInfo, inResult);
                }
            }
        }

        /** Checks if the parent object has the value needed to run validation */
        private bool ParentHasRequiredValue(object inInstance)
        {
            BindingFlags searchFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            System.Type parentType = inInstance.GetType();
            MemberInfo[] memberInfos = parentType.GetMember(Attribute.MemberName, searchFlags);
            
            if (memberInfos.Length == 0)
            {
                throw new MemberNotFoundException($"Could not find a member with the name {Attribute.MemberName}"); 
            } 
            if (memberInfos.Length > 1)
            {
                throw new MemberNotFoundException($"Multiple members exist with the name {Attribute.MemberName}. " +
                                                   $"Please choose a variable name that corresponds to exactly one member");
            }

            MemberInfo memberInfo = memberInfos[0];

            if (memberInfo.MemberType.HasFlag(MemberTypes.Field) == true)
            {
                return parentType.GetField(Attribute.MemberName, searchFlags).GetValue(inInstance).Equals(Attribute.Value);
            }

            if (memberInfo.MemberType.HasFlag(MemberTypes.Property) == true)
            {
                return parentType.GetProperty(Attribute.MemberName, searchFlags).GetValue(inInstance).Equals(Attribute.Value);
            }
            
            if (memberInfo.MemberType.HasFlag(MemberTypes.Method) == true)
            {
                return parentType.GetMethod(Attribute.MemberName, searchFlags).Invoke(inInstance, null).Equals(Attribute.Value);
            }
            
            throw new MemberTypeNotSupportedException($"Specified member was of the type: " +
                                                      $"{memberInfo.MemberType} and is not supported by " +
                                                      $"RequireIfAttribute. The member specified by RequireIfAttribute " +
                                                      $"must be a field, a property, or a 0-argument method.");
        }

        /* Validates that the object meets the constraints for Required fields specified in Odin's RequiredValidator */
        private bool OdinValidateRequiredObject(System.Object inObject)
        {
            // RG: this code is copied from the Odin source for RequiredValidator. Delegating
            // to that class is not possible because AttributeValidators require some setup that's taking care of Odin
            // internally.
            //
            // If we can delegate to RequiredValidator in the future, we should. Otherwise, just make sure
            // this code stays current with the version of Odin we're using

            if (object.ReferenceEquals(inObject, null) == true)
            {
                return false;
            }

            if (inObject is string == true && string.IsNullOrEmpty((string) inObject) == true)
            {
                return false;
                
            }

            if (inObject is UnityEngine.Object == true && (inObject as UnityEngine.Object) == null)
            {
                return false;
                
            }
            
            return true;
        }

        /*
         * Modifies the given ValidationResult with the error message specified by the user, or a default settings for
         * each setting not specified by the user
         */
        private void OdinValidationFailed(object inParentInstance, MemberInfo inMemberInfo, ValidationResult inResult)
        {
            // RG: this code is copied from the Odin source for RequiredValidator. Delegating
            // to that class is not possible because AttributeValidators require some setup that's taking care of Odin
            // internally.
            //
            // If we can delegate to RequiredValidator in the future, we should. Otherwise, just make sure
            // this code stays current with the version of Odin we're using
            inResult.ResultType = Attribute.RequiredAttribute.MessageType.ToValidationResultType();
            if (string.IsNullOrEmpty(Attribute.RequiredAttribute.ErrorMessage) == true)
            {
                inResult.Message = _StringMemberHelper != null ? _StringMemberHelper.GetValue() : inMemberInfo.Name + " is required";    
            } 
            else
            {
                inResult.Message = Attribute.RequiredAttribute.ErrorMessage;
            }
        }
    }   
}
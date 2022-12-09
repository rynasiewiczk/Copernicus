using LazySloth.Editor;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(RequiredOnSceneAttributeValidator))]
namespace LazySloth.Editor
{
    using System;
    using System.Reflection;
    using Sirenix.OdinInspector.Editor.ValueResolvers;
    
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class RequiredOnSceneAttributeValidator : AttributeValidator<RequiredOnSceneAttribute>
    {
        private ValueResolver<string> _StringMemberHelper;

        public override void Initialize(MemberInfo member, Type memberValueType)
        {
            if (Attribute.ErrorMessage != null)
            {
                    _StringMemberHelper = ValueResolver.Get<string>(Property, Attribute.ErrorMessage);
                
            }
        }
        
        protected override void Validate(object parentInstance, object memberValue, MemberInfo member, ValidationResult result)
        {
            if (!(parentInstance is MonoBehaviour)) return;
            
            MonoBehaviour parent = (MonoBehaviour) parentInstance;
            GameObject sourceObject = parent.gameObject;
            
            bool isInPrefabMode = UnityEditor.SceneManagement.PrefabStageUtility.GetPrefabStage(sourceObject) != null;
            bool memberValid = IsValid(memberValue);
            var scene = sourceObject.scene;
            bool validScene = scene.IsValid() && scene.isLoaded;
            bool valid = isInPrefabMode || memberValid || !validScene;

            if (valid) return;
            result.ResultType = ValidationResultType.Error;
            result.Message = _StringMemberHelper != null ? _StringMemberHelper.GetValue() : member.Name+" is required on scene!";
        }
        
        private static bool IsValid(object memberValue)
        {
            switch (memberValue)
            {
                case null:
                case string value when string.IsNullOrEmpty(value):
                case Object o when o == null:
                    return false;
                default:
                    return true;
            }
        }
    }
}
namespace LazySloth
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredOnSceneAttribute : Attribute
    {
        // ReSharper disable once UnassignedField.Global
        public string ErrorMessage;
    }
}
namespace LazySloth
{
    using UnityEngine;

    public class AnimationNameAttribute : PropertyAttribute
    {
        public string DataField { get; }

        public AnimationNameAttribute(string dataField)
        {
            DataField = dataField;
        }
    }
}
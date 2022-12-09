namespace LazySloth
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(AnimationNameAttribute), true)]
    public class AnimationNameAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var animationAttribute = attribute as AnimationNameAttribute;
            if(animationAttribute == null) { return; }
            
            var relativeProperty = property.serializedObject.FindProperty(animationAttribute.DataField);
            if(relativeProperty == null) { return; }

            var reference = relativeProperty.objectReferenceValue;
            var animationComponent = reference as Animation;
            
            if(animationComponent == null) { return; }
            
            var clipNames = new List<string>();
            
            var assetObject = new SerializedObject(reference);
            var clipsList = assetObject.FindProperty("m_Animations");
            for (var i = 0; i < clipsList.arraySize; i++)
            {
                var clip = clipsList.GetArrayElementAtIndex(i);
                if(clip.objectReferenceValue == null) { continue; }
                clipNames.Add((clip.objectReferenceValue as AnimationClip).name);
            }

            if(!clipNames.Any()) { return; }
            
            var clipName = "";
            var labelText = label.text;

            var index = clipNames.IndexOf(property.stringValue);

            if (index == -1)
            {
                index = 0;
                position.height -= 16;
            }

            position.width -= 82;
            index = EditorGUI.Popup(position, labelText, index, clipNames.ToArray());
            clipName = clipNames[index];
            property.stringValue = clipName;
        }
    }
}
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

internal class EndNameEdit : EndNameEditAction
{
	#region implemented abstract members of EndNameEditAction
	public override void Action (int instanceId, string pathName, string resourceFile)
	{
		AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId), AssetDatabase.GenerateUniqueAssetPath(pathName));
	}

	#endregion
}

public class ScriptableObjectWindow : OdinEditorWindow
{
	
	private Type[] types;

	private IEnumerable GetTypes()
	{
		return types;
	}

	[ShowInInspector, ValueDropdown(nameof(GetTypes))]
	private Type typeToCreate = null;

	protected override void Initialize()
	{
		base.Initialize();
		var assembly = Assembly.Load(new AssemblyName("Assembly-CSharp"));

		types = GetSosFromAssembly(assembly);

		Type[] GetSosFromAssembly(Assembly assembly1)
		{
			return assembly1.GetTypes().Where(t => t.IsSubclassOf(typeof(ScriptableObject))).ToArray();
		}
	}

	[Button(ButtonSizes.Large)]
	private void CreateSO()
	{
		var asset = ScriptableObject.CreateInstance(typeToCreate);
		ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
			asset.GetInstanceID(),
			ScriptableObject.CreateInstance<EndNameEdit>(),
			string.Format("{0}.asset", typeToCreate.Name),
			AssetPreview.GetMiniThumbnail(asset),
			null);

		Close();
	}
	
	[MenuItem("Project/Create Scriptable Object _F10")]
	public static void Open()
	{
		GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true).ShowPopup();
	}
}
namespace LazySloth.Utilities.Editor
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;
    using Utilities;

    public abstract class ExportToTsv<T> : OdinEditorWindow where T : Object
    {
        protected abstract string LocalizationTab { get; }

        [SerializeField] private List<T> _dataToExport = default;

        [SerializeField, Sirenix.OdinInspector.FilePath(AbsolutePath = true)] private string _destination = default;
        
        [ShowInInspector]
        private void Export()
        {
            var file = new TsvFile();
            for (var i = 0; i < _dataToExport.Count; i++)
            {
                file.AddLines(ProcessData(_dataToExport[i]));
                EditorUtility.SetDirty(_dataToExport[i]);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            file.Save(_destination);
        }

        protected abstract List<TsvLine> ProcessData(T data);

        protected string CreateLocalizationTerm(string rawKey) => LocalizationTab + rawKey;
    }
}
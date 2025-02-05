using UnityEditor;
using UnityEngine;

namespace CodeBase.SaveSystem.Editor
{
    [CustomEditor(typeof(SaveLoadSystem))]
    public class SaveLoadEditor:UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SaveLoadSystem saveLoadSystem = (SaveLoadSystem) target;
            string saveName = saveLoadSystem.MapData.Name;
            
            DrawDefaultInspector();

            if (GUILayout.Button("New Launch"))
            {
                saveLoadSystem.NewLaunch();
            }
            if (GUILayout.Button("Save Data"))
            {
                saveLoadSystem.Save();
            }

            if (GUILayout.Button("Load Data"))
            {
                saveLoadSystem.Load(saveName);
            }

            if (GUILayout.Button("Delete Data"))
            {
                saveLoadSystem.DeleteAll();
            }
        }
    }
}
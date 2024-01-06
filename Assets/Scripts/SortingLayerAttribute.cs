#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class SortingLayerAttribute : PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SortingLayerAttribute))]
public class SortingLayerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginChangeCheck();

            int selectedLayerIndex = GetSortingLayerIndex(property.stringValue);
            selectedLayerIndex = EditorGUI.Popup(position, label.text, selectedLayerIndex, GetSortingLayerNames());

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = GetSortingLayerName(selectedLayerIndex);
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use SortingLayer with string.");
        }

        EditorGUI.EndProperty();
    }

    private string[] GetSortingLayerNames()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty sortingLayers = tagManager.FindProperty("m_SortingLayers");

        string[] layerNames = new string[sortingLayers.arraySize];

        for (int i = 0; i < sortingLayers.arraySize; i++)
        {
            SerializedProperty layer = sortingLayers.GetArrayElementAtIndex(i);
            layerNames[i] = layer.FindPropertyRelative("name").stringValue;
        }

        return layerNames;
    }

    private int GetSortingLayerIndex(string layerName)
    {
        string[] layerNames = GetSortingLayerNames();
        return System.Array.IndexOf(layerNames, layerName);
    }

    private string GetSortingLayerName(int index)
    {
        string[] layerNames = GetSortingLayerNames();
        if (index >= 0 && index < layerNames.Length)
        {
            return layerNames[index];
        }
        return string.Empty;
    }
}
#endif

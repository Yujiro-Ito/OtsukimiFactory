using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateScriptable : MonoBehaviour {

	[MenuItem("Assets/Create/RabbitData")]
    public static void CreateAsset()
    {
        CreateAsset<RabbitData>();
    }

    public static void CreateAsset<Type>() where Type : ScriptableObject{
        Type item = ScriptableObject.CreateInstance<Type>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/RabbitDatas/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }
}

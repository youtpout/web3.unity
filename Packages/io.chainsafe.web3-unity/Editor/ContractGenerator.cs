using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using ChainSafe.Gaming.Generators;

public class ContractGenerator : EditorWindow
{
    static string path = "Assets/Scripts/Contracts/";
    static string contractName = "ExempleContract";
    static string abi = "";

    [MenuItem("ChainSafe SDK/Generate Contracts Classes")]
    public static void GenerateContract()
    {
        ContractGenerator window = CreateInstance<ContractGenerator>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 640, 300);
        window.ShowUtility();
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        contractName = EditorGUILayout.TextField("Set a class name : ", contractName);
        GUILayout.Space(10);
        abi = EditorGUILayout.TextField("Paste your abi here : ", abi);
        GUILayout.Space(60);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            var result = ContractGenerator.GenerateContract(abi, contractName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = $"{Path.Combine(path, item.FileName)}";
            File.WriteAllText(filePath, result);
            Debug.Log($"File generated : {filePath}");

            Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }

}

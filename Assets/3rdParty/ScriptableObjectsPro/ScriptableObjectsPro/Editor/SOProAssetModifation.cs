using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SOProAssetModifation : AssetModificationProcessor
{
    public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions rao)
    {
        if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) is ScriptableObjectPRO soPro)
        {
            SOProMono.Instance.SoProData.ScriptableObjectPros.Remove(soPro);
            
            UnityEditor.EditorUtility.SetDirty(  SOProMono.Instance.SoProData);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
        return AssetDeleteResult.DidNotDelete;
    }

    public static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
    {
        return AssetMoveResult.DidNotMove;
    }

    public static async void OnWillCreateAsset(string assetPath)
    {
        await Task.Delay(50);
        if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) is ScriptableObjectPRO soPro)
        {
            soPro.ConvertToSOPRO();
        }
    }
}
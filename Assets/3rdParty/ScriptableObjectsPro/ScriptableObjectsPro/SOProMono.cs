using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SOProMono : Singleton<SOProMono>
{
    private List<ScriptableObjectPRO> ScriptableObjectPros;

    public SOProData SoProData;

    private void Awake()
    {
        ScriptableObjectPros = SoProData.ScriptableObjectPros.Where(item => item != null).ToList();

        foreach (ScriptableObjectPRO soPro in ScriptableObjectPros)
        {
            soPro.OnAwake();
        }
    }

    private IEnumerator Start()
    {
        foreach (ScriptableObjectPRO soPro in ScriptableObjectPros)
        {
            soPro.OnStart();
            StartCoroutine(OnUpdate(soPro));
        }

        yield break;
    }

    // private void Update()
    // {
    //     for (var i = 0; i < ScriptableObjectPros.Count; i++)
    //     {
    //         ScriptableObjectPros[i].OnUpdate();
    //     }
    // }
    private void OnDisable()
    {
        foreach (ScriptableObjectPRO soPro in ScriptableObjectPros)
        {
            soPro.OnDisable();
        }
    }

    IEnumerator OnUpdate(ScriptableObjectPRO scriptableObjectPro)
    {
        while (scriptableObjectPro.OnUpdateInterval >= 0)
        {
            scriptableObjectPro.OnUpdate();

            yield return new WaitForSeconds(scriptableObjectPro.OnUpdateInterval);
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        foreach (ScriptableObjectPRO soPro in ScriptableObjectPros)
        {
            soPro.OnApplicationFocusChanged(hasFocus);
        }
    }

    private void OnApplicationQuit()
    {
        foreach (ScriptableObjectPRO soPro in ScriptableObjectPros)
        {
            soPro.OnApplicationQuit();
        }
    }
}

public class ScriptableObjectPRO : ScriptableObject
{
    [Header("OnUpdate Frequency")] public float OnUpdateInterval;
    internal SOProMono _soProMono => SOProMono.Instance;

    public bool IsSOPro => _soProMono != null && _soProMono.SoProData.ScriptableObjectPros.Contains(this);

    public virtual void OnAwake()
    {
    }

    public virtual void OnStart()
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnApplicationFocusChanged(bool focus)
    {
    }

    public virtual void OnApplicationQuit()
    {
    }
#if UNITY_EDITOR
    public bool ConvertToSOPRO()
    {

        List<ScriptableObjectPRO> scriptableObjectPros = _soProMono.SoProData.ScriptableObjectPros;

        try
        {
            if (scriptableObjectPros.Contains(this))
            {
                return false;
            }

            /* Add me to list */
            scriptableObjectPros.Add(this);

            /* Save the so */
            UnityEditor.EditorUtility.SetDirty(_soProMono.SoProData);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            Debug.Log($"<color=#5fe769><b>Converted to Scriptable Object Pro!</b> </color>");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("It was not converted to SO pro, there is no SOMono in the scene." + e);
            return false;
        }
    }
#endif

}
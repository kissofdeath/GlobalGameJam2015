using System;
using UnityEditor;
using UnityEngine;

class MassMaterialEditor : EditorWindow
{
    static MassMaterialEditor window;
    Material src, dst;

    [MenuItem("Scene Editor/Mass Material Editor")]
    static void Execute()
    {
        if (window == null)
            window = (MassMaterialEditor)GetWindow(typeof(MassMaterialEditor));
        window.Show();
    }

    void OnGUI()
    {            
        GUILayout.Label("Selected Materials are modified");
        src = (Material)EditorGUILayout.ObjectField("src mat", src, typeof(Material), true);
        dst = (Material)EditorGUILayout.ObjectField("dst mat", dst, typeof(Material), true);

        if (GUILayout.Button("Replace"))
        {
            if (src != null & dst != null && dst != src)
            {
                Debug.Log("Gonna go through the scene...");
                // go through scene and edit replace src with dst on all applicable game objects
                GameObject[] gobjs = GameObject.FindObjectsOfType<GameObject>();

                Debug.Log(gobjs.Length);
                foreach (GameObject gobj in gobjs)
                {

                    SpriteRenderer r = gobj.GetComponent<SpriteRenderer>();
                    Debug.Log(r);
                    
                    if (r != null && r.sharedMaterial.shader == src.shader)
                    {
                        Debug.Log("Changing stuff for " + r.gameObject.name);
                        r.sharedMaterial = dst;
                    }
                }

            }
        }
    }

    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComebinMesheDemo : MonoBehaviour
{
    void Start()
    {
        //获取所有网格过滤器;
        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        //存放不同的材质球，相同的就存一个;
        Dictionary<string, Material> materials = new Dictionary<string, Material>();
        //存放要合并的网格对象;
        Dictionary<string, List<CombineInstance>> combines = new Dictionary<string, List<CombineInstance>>();
        for (int i = 0; i < meshFilters.Length; i++)
        {
            //构造一个网格合并结构体;
            CombineInstance combine = new CombineInstance();
            //给结构体的mesh赋值;
            combine.mesh = meshFilters[i].mesh;
            combine.transform = meshFilters[i].transform.localToWorldMatrix;
            MeshRenderer renderer = meshFilters[i].GetComponent<MeshRenderer>();
            Material mat = renderer.sharedMaterial;
            if (!materials.ContainsKey(mat.name))
            {
                materials.Add(mat.name, mat);
            }
            if (combines.ContainsKey(mat.name))
            {
                combines[mat.name].Add(combine);
            }
            else
            {
                List<CombineInstance> coms = new List<CombineInstance>();
                coms.Add(combine);
                combines[mat.name] = coms;
            }
            Destroy(meshFilters[i].gameObject);
        }
        GameObject combineObj = new GameObject("Combine");
        combineObj.transform.parent = transform;
        foreach (KeyValuePair<string, Material> mater in materials)
        {
            GameObject obj = new GameObject(mater.Key);
            obj.transform.parent = combineObj.transform;
            MeshFilter combineMeshFilter = obj.AddComponent<MeshFilter>();
            combineMeshFilter.mesh = new Mesh();
            //将引用相同材质球的网格合并;
            combineMeshFilter.mesh.CombineMeshes(combines[mater.Key].ToArray(), true, true);
            MeshRenderer rend = obj.AddComponent<MeshRenderer>();
            //指定材质球;
            rend.sharedMaterial = mater.Value;
            //rend.castShadows = false;
            rend.receiveShadows = true;

        }
    }
}

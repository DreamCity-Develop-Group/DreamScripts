using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComebinMesheDemo : MonoBehaviour
{
    void Start()
    {
        //��ȡ�������������;
        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        //��Ų�ͬ�Ĳ�������ͬ�ľʹ�һ��;
        Dictionary<string, Material> materials = new Dictionary<string, Material>();
        //���Ҫ�ϲ����������;
        Dictionary<string, List<CombineInstance>> combines = new Dictionary<string, List<CombineInstance>>();
        for (int i = 0; i < meshFilters.Length; i++)
        {
            //����һ������ϲ��ṹ��;
            CombineInstance combine = new CombineInstance();
            //���ṹ���mesh��ֵ;
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
            //��������ͬ�����������ϲ�;
            combineMeshFilter.mesh.CombineMeshes(combines[mater.Key].ToArray(), true, true);
            MeshRenderer rend = obj.AddComponent<MeshRenderer>();
            //ָ��������;
            rend.sharedMaterial = mater.Value;
            //rend.castShadows = false;
            rend.receiveShadows = true;

        }
    }
}

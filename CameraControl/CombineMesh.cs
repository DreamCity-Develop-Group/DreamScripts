using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CombineMeshs();
    }

    void CombineMeshs()
    {

        MeshFilter[] mfChildren = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[mfChildren.Length];


        MeshRenderer[] mrChildren = GetComponentsInChildren<MeshRenderer>();
        Material[] materials = new Material[mrChildren.Length];


        MeshRenderer mrSelf = gameObject.AddComponent<MeshRenderer>();
        MeshFilter mfSelf = gameObject.AddComponent<MeshFilter>();


        Texture2D[] textures = new Texture2D[mrChildren.Length];
        for (int i = 0; i < mrChildren.Length; i++)
        {
            if (mrChildren[i].transform == transform)
            {
                continue;
            }
            materials[i] = mrChildren[i].sharedMaterial;
            Texture2D tx = materials[i].GetTexture("_MainTex") as Texture2D;


            Texture2D tx2D = new Texture2D(tx.width, tx.height, TextureFormat.ARGB32, false);
            tx2D.SetPixels(tx.GetPixels(0, 0, tx.width, tx.height));
            tx2D.Apply();
            textures[i] = tx2D;
        }


        Material materialNew = new Material(materials[0].shader);
        materialNew.CopyPropertiesFromMaterial(materials[0]);
        mrSelf.sharedMaterial = materialNew;


        Texture2D texture = new Texture2D(1024, 1024);
        materialNew.SetTexture("_MainTex", texture);
        Rect[] rects = texture.PackTextures(textures, 10, 1024);


        for (int i = 0; i < mfChildren.Length; i++)
        {
            if (mfChildren[i].transform == transform)
            {
                continue;
            }
            Rect rect = rects[i];


            Mesh meshCombine = mfChildren[i].mesh;
            Vector2[] uvs = new Vector2[meshCombine.uv.Length];
            //�������uv������ͼ��rectˢһ��
            for (int j = 0; j < uvs.Length; j++)
            {
                uvs[j].x = rect.x + meshCombine.uv[j].x * rect.width;
                uvs[j].y = rect.y + meshCombine.uv[j].y * rect.height;
            }
            meshCombine.uv = uvs;
            combine[i].mesh = meshCombine;
            combine[i].transform = mfChildren[i].transform.localToWorldMatrix;
            mfChildren[i].gameObject.SetActive(false);
        }


        Mesh newMesh = new Mesh();
        newMesh.CombineMeshes(combine, true, true);//�ϲ�����
        mfSelf.mesh = newMesh;
    }

}

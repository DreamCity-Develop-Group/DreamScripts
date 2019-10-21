using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowingObject : MonoBehaviour
{
    [SerializeField]
    GameObject worldPos;//3D物体(建筑）
    private RectTransform rectTrans;//UI元素
    public Vector2 offset;//偏移量
    private Button OnLike;  //点赞

    private void Start()
    {
        rectTrans = transform.GetComponent<RectTransform>();
        OnLike = rectTrans.GetComponent<Button>();
        OnLike.onClick.AddListener(OnClickLike);
    }
    void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos.transform.position);
        rectTrans.position = screenPos + offset;
    }
    private void OnClickLike()
    {
        rectTrans.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
/***
  * Title:     
  *
  * Created:	#AuthorName#
  *
  * CreatTime:          #CreateTime#
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
public class InitSource : MonoBehaviour
{
    private Image head0Image;
    private Image head1Image;
    private Image head2Image;
    private Image head3Image;
    private Image head4Image;
    private Image head5Image;
    private Image head6Image;
    private Image head7Image;

   

    // Start is called before the first frame update
    void Start()
    {
        head0Image = transform.Find("MenuPanel/HandFrame/Frame/Hand0").GetComponent<Image>();
        head1Image = transform.Find("MenuPanel/HandFrame/Frame/Hand1").GetComponent<Image>();
        head2Image = transform.Find("MenuPanel/HandFrame/Frame/Hand2").GetComponent<Image>();
        head3Image = transform.Find("MenuPanel/HandFrame/Frame/Hand3").GetComponent<Image>();
        head4Image = transform.Find("MenuPanel/HandFrame/Frame/Hand4").GetComponent<Image>();
        head5Image = transform.Find("MenuPanel/HandFrame/Frame/Hand5").GetComponent<Image>();
        head6Image = transform.Find("MenuPanel/HandFrame/Frame/Hand6").GetComponent<Image>();
        head7Image = transform.Find("MenuPanel/HandFrame/Frame/Hand7").GetComponent<Image>();

        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("UI/HeadSpriteAlta");
        head0Image.sprite = spriteAtlas.GetSprite("Head0");
        head1Image.sprite = spriteAtlas.GetSprite("Head1");
        head2Image.sprite = spriteAtlas.GetSprite("Head2");
        head3Image.sprite = spriteAtlas.GetSprite("Head3");
        head4Image.sprite = spriteAtlas.GetSprite("Head4");
        head5Image.sprite = spriteAtlas.GetSprite("Head5");
        head6Image.sprite = spriteAtlas.GetSprite("Head6");
        head7Image.sprite = spriteAtlas.GetSprite("Head7");
        //sprite 
        //Sprite[] spriteArray = new Sprite[spriteAtlas.spriteCount];
        ////spriteArray得到数组
        //spriteAtlas.GetSprites(spriteArray);

    }

    //    AssetBundle assetbundle = null;
    //    void Start()
    //    {
    //        CreatImage(loadSprite("image0"));
    //        CreatImage(loadSprite("image1"));
    //    }

    //    private void CreatImage(Sprite sprite)
    //    {
    //        GameObject go = new GameObject(sprite.name);
    //        go.layer = LayerMask.NameToLayer("UI");
    //        go.transform.parent = transform;
    //        go.transform.localScale = Vector3.one;
    //        Image image = go.AddComponent<Image>();
    //        image.sprite = sprite;
    //        image.SetNativeSize();
    //    }

    //    private Sprite loadSprite(string spriteName)
    //    {
    //#if USE_ASSETBUNDLE
    //		if(assetbundle == null)
    //			assetbundle = AssetBundle.CreateFromFile(Application.streamingAssetsPath +"/Main.assetbundle");
    //				return assetbundle.Load(spriteName) as Sprite;
    //#else
    //        return Resources.Load<GameObject>("Sprite/" + spriteName).GetComponent<SpriteRenderer>().sprite;
    //#endif
    //    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

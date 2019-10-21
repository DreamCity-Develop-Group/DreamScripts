using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

public class HelpPanel : UIBase
{
    private Button RelationType;                   //联系方式
    private Button ChamberType;                   //商会类型
    private Button InvestmentType;                //投资类型
    private Button AssetType;                    //资产类型

    private GameObject RAnswer;
    private Transform TranRo;
    private Transform TranType1;
    private Transform TranType2;
    private Transform TranType3;

    private GameObject[] TypeOneQ = new GameObject[6];
    private Button[] TypeOneQBtn = new Button[6];
    private Transform[] tranTOne = new Transform[6];
    private GameObject[] TypeOneA = new GameObject[6];
    private GameObject[] TypeTwoQ = new GameObject[5];
    private Transform[] traTTwo = new Transform[5];
    private Button[] TypeTwoQBtn = new Button[5];
    private GameObject[] TypeTwoA = new GameObject[5];
    private GameObject[] TypeThreeQ = new GameObject[9];
    private Transform[] tranTThree = new Transform[9];
    private Button[] TypeThreeQBtn = new Button[9];
    private GameObject[] TypeThreeA = new GameObject[9];


    private bool IsClickRela = false;            //是否点击了联系
    private bool IsClickTypeOne = false;         //是否点击了类型1
    private bool IsClickTypeTwo = false;         //是否点击了类型2
    private bool IsClickTypeThree = false;       //是否点击了类型3

    private bool OQuestion1 = false;
    private bool OQuestion2 = false;
    private bool OQuestion3 = false;
    private bool OQuestion4 = false;
    private bool OQuestion5 = false;
    private bool OQuestion6 = false;

    private bool TQuestion1 = false;
    private bool TQuestion2 = false;
    private bool TQuestion3 = false;
    private bool TQuestion4 = false;
    private bool TQuestion5 = false;

    private bool HQuestion1 = false;
    private bool HQuestion2 = false;
    private bool HQuestion3 = false;
    private bool HQuestion4 = false;
    private bool HQuestion5 = false;
    private bool HQuestion6 = false;
    private bool HQuestion7 = false;
    private bool HQuestion8 = false;
    private bool HQuestion9 = false;
    private static HelpPanel instance;
    public static HelpPanel Instance
    {
        get { return instance; }
    }




    void Awake()
    {
        instance = this;
        RelationType = transform.Find("Scroll View/Viewport/Content/Relation").GetComponent<Button>();
        ChamberType = transform.Find("Scroll View/Viewport/Content/Type1").GetComponent<Button>();
        InvestmentType = transform.Find("Scroll View/Viewport/Content/Type2").GetComponent<Button>();
        AssetType = transform.Find("Scroll View/Viewport/Content/Type3").GetComponent<Button>();
        RAnswer = transform.Find("Scroll View/Viewport/Content/RAnswer").gameObject;
        TranRo = RelationType.transform.Find("Image");
        TranType1 = ChamberType.transform.Find("Image");
        TranType2 = InvestmentType.transform.Find("Image");
        TranType3 = AssetType.transform.Find("Image");
        for (int i = 0; i < TypeOneQ.Length; i++)
        {
            TypeOneQ[i] = transform.Find("Scroll View/Viewport/Content/OQuestion" + (i + 1)).gameObject;
            TypeOneA[i]= transform.Find("Scroll View/Viewport/Content/OAnswer" + (i + 1)).gameObject;
            TypeOneQBtn[i] = TypeOneQ[i].GetComponent<Button>();
            tranTOne[i] = TypeOneQ[i].transform.Find("Image");
        }
        for (int i = 0; i < TypeTwoQ.Length; i++)
        {
            TypeTwoQ[i]= transform.Find("Scroll View/Viewport/Content/TQuestion" + (i + 1)).gameObject;
            TypeTwoA[i]= transform.Find("Scroll View/Viewport/Content/TAnswer" + (i + 1)).gameObject;
            TypeTwoQBtn[i] = TypeTwoQ[i].GetComponent<Button>();
            traTTwo[i] = TypeTwoQ[i].transform.Find("Image");
        }
        for (int i = 0; i < TypeThreeQ.Length; i++)
        {
            TypeThreeQ[i]= transform.Find("Scroll View/Viewport/Content/HQuestion" + (i + 1)).gameObject;
            TypeThreeA[i]= transform.Find("Scroll View/Viewport/Content/HAnswer" + (i + 1)).gameObject;
            TypeThreeQBtn[i] = TypeThreeQ[i].GetComponent<Button>();
            tranTThree[i] = TypeThreeQ[i].transform.Find("Image");
        }

        RelationType.onClick.AddListener(ClickRelationType);
        ChamberType.onClick.AddListener(ClickTypeOne);
        InvestmentType.onClick.AddListener(ClickTypeTwo);
        AssetType.onClick.AddListener(ClickTypeThree);
        TypeOneQBtn[0].onClick.AddListener(TypeOneQ1);
        TypeOneQBtn[1].onClick.AddListener(TypeOneQ2);
        TypeOneQBtn[2].onClick.AddListener(TypeOneQ3);
        TypeOneQBtn[3].onClick.AddListener(TypeOneQ4);
        TypeOneQBtn[4].onClick.AddListener(TypeOneQ5);
        TypeOneQBtn[5].onClick.AddListener(TypeOneQ6);
        TypeTwoQBtn[0].onClick.AddListener(TypeTwoQ1);
        TypeTwoQBtn[1].onClick.AddListener(TypeTwoQ2);
        TypeTwoQBtn[2].onClick.AddListener(TypeTwoQ3);
        TypeTwoQBtn[3].onClick.AddListener(TypeTwoQ4);
        TypeTwoQBtn[4].onClick.AddListener(TypeTwoQ5);
        TypeThreeQBtn[0].onClick.AddListener(TypeThreeQ1);
        TypeThreeQBtn[1].onClick.AddListener(TypeThreeQ2);
        TypeThreeQBtn[2].onClick.AddListener(TypeThreeQ3);
        TypeThreeQBtn[3].onClick.AddListener(TypeThreeQ4);
        TypeThreeQBtn[4].onClick.AddListener(TypeThreeQ5);
        TypeThreeQBtn[5].onClick.AddListener(TypeThreeQ6);
        TypeThreeQBtn[6].onClick.AddListener(TypeThreeQ7);
        TypeThreeQBtn[7].onClick.AddListener(TypeThreeQ8);
        TypeThreeQBtn[8].onClick.AddListener(TypeThreeQ9);
    }
    private void Start()
    {
        RAnswer.SetActive(false);
        Initialize();
    }
    public void Initialize()
    {
        TranType1.rotation = Quaternion.Euler(0, 0, 0);
        TranType2.rotation = Quaternion.Euler(0, 0, 0);
        TranType3.rotation = Quaternion.Euler(0, 0, 0);
        IsClickRela = false;            //是否点击了联系
        IsClickTypeOne = false;         //是否点击了类型1
        IsClickTypeTwo = false;         //是否点击了类型2
        IsClickTypeThree = false;       //是否点击了类型3

        OQuestion1 = false;
        OQuestion2 = false;
        OQuestion3 = false;
        OQuestion4 = false;
        OQuestion5 = false;
        OQuestion6 = false;

        TQuestion1 = false;
        TQuestion2 = false;
        TQuestion3 = false;
        TQuestion4 = false;
        TQuestion5 = false;

        HQuestion1 = false;
        HQuestion2 = false;
        HQuestion3 = false;
        HQuestion4 = false;
        HQuestion5 = false;
        HQuestion6 = false;
        HQuestion7 = false;
        HQuestion8 = false;
        HQuestion9 = false;
        RAnswer.SetActive(false);
        for (int i = 0; i < TypeOneA.Length; i++)
        {
            TypeOneQ[i].SetActive(false);
            TypeOneA[i].SetActive(false);
            tranTOne[i].rotation = Quaternion.Euler(0, 0, 0);
        }
        for (int i = 0; i < TypeTwoQ.Length; i++)
        {
            TypeTwoQ[i].SetActive(false);
            TypeTwoA[i].SetActive(false);
            traTTwo[i].rotation = Quaternion.Euler(0, 0, 0);
        }
        for (int i = 0; i < TypeThreeQ.Length; i++)
        {
            TypeThreeQ[i].SetActive(false);
            TypeThreeA[i].SetActive(false);
            tranTThree[i].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    /// <summary>
    /// 联系
    /// </summary>
    private void ClickRelationType()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!IsClickRela)
        {
            RAnswer.SetActive(true);
            IsClickRela = true;
            TranRo.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            RAnswer.SetActive(false);
            IsClickRela = false;
            TranRo.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    /// <summary>
    /// 商会类型
    /// </summary>
    private void ClickTypeOne()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!IsClickTypeOne)
        {
            for (int i = 0; i < TypeOneQ.Length; i++)
            {
                TypeOneQ[i].SetActive(true);
            }
            IsClickTypeOne = true;
            TranType1.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            for (int i = 0; i < TypeOneQ.Length; i++)
            {
                TypeOneQ[i].SetActive(false);
                TypeOneA[i].SetActive(false);
            }
            IsClickTypeOne = false;
            TranType1.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    /// <summary>
    /// 投资类型
    /// </summary>
    private void ClickTypeTwo()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!IsClickTypeTwo)
        {
            for (int i = 0; i < TypeTwoQ.Length; i++)
            {
                TypeTwoQ[i].SetActive(true);
            }
            IsClickTypeTwo = true;
            TranType2.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            for (int i = 0; i < TypeTwoQ.Length; i++)
            {
                TypeTwoQ[i].SetActive(false);
                TypeTwoA[i].SetActive(false);
            }
            IsClickTypeTwo = false;
            TranType2.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    /// <summary>
    /// 资产类型
    /// </summary>
    private void ClickTypeThree()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!IsClickTypeThree)
        {
            for (int i = 0; i < TypeThreeQ.Length; i++)
            {
                TypeThreeQ[i].SetActive(true);
            }
            IsClickTypeThree = true;
            TranType3.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            for (int i = 0; i < TypeThreeQ.Length; i++)
            {
                TypeThreeQ[i].SetActive(false);
                TypeThreeA[i].SetActive(false);
            }
            IsClickTypeThree = false;
            TranType3.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeOneQ1()
    {
         Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if(!OQuestion1)
        {
            TypeOneA[0].SetActive(true);
            OQuestion1 = true;
            tranTOne[0].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeOneA[0].SetActive(false);
            OQuestion1 = false;
            tranTOne[0].rotation = Quaternion.Euler(0, 0, 0);
        }
    }   
    private void TypeOneQ2()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!OQuestion2)
        {
            TypeOneA[1].SetActive(true);
            OQuestion2 = true;
            tranTOne[1].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeOneA[1].SetActive(false);
            OQuestion2 = false;
            tranTOne[1].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeOneQ3()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!OQuestion3)
        {
            TypeOneA[2].SetActive(true);
            OQuestion3 = true;
            tranTOne[2].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeOneA[2].SetActive(false);
            OQuestion3 = false;
            tranTOne[2].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeOneQ4()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!OQuestion4)
        {
            TypeOneA[3].SetActive(true);
            OQuestion4 = true;
            tranTOne[3].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeOneA[3].SetActive(false);
            OQuestion4 = false;
            tranTOne[3].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeOneQ5()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!OQuestion5)
        {
            TypeOneA[4].SetActive(true);
            OQuestion5 = true;
            tranTOne[4].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeOneA[4].SetActive(false);
            OQuestion5 = false;
            tranTOne[1].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeOneQ6()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!OQuestion6)
        {
            TypeOneA[5].SetActive(true);
            OQuestion6 = true;
            tranTOne[5].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeOneA[5].SetActive(false);
            OQuestion6 = false;
            tranTOne[5].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeTwoQ1()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!TQuestion1)
        {
            TypeTwoA[0].SetActive(true);
            TQuestion1 = true;
            traTTwo[0].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeTwoA[0].SetActive(false);
            TQuestion1 = false;
            traTTwo[0].rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void TypeTwoQ2()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!TQuestion2)
        {
            TypeTwoA[1].SetActive(true);
            TQuestion2 = true;
            traTTwo[1].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeTwoA[1].SetActive(false);
            TQuestion2 = false;
            traTTwo[1].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeTwoQ3()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!TQuestion3)
        {
            TypeTwoA[2].SetActive(true);
            TQuestion3 = true;
            traTTwo[2].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeTwoA[2].SetActive(false);
            TQuestion3 = false;
            traTTwo[2].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeTwoQ4()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!TQuestion4)
        {
            TypeTwoA[3].SetActive(true);
            TQuestion4 = true;
            traTTwo[3].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeTwoA[3].SetActive(false);
            TQuestion4 = false;
            traTTwo[3].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeTwoQ5()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!TQuestion5)
        {
            TypeTwoA[4].SetActive(true);
            TQuestion5 = true;
            traTTwo[4].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeTwoA[4].SetActive(false);
            TQuestion5 = false;
            traTTwo[4].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ1()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion1)
        {
            TypeThreeA[0].SetActive(true);
            HQuestion1 = true;
            tranTThree[0].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[0].SetActive(false);
            HQuestion1 = false;
            tranTThree[0].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ2()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion2)
        {
            TypeThreeA[1].SetActive(true);
            HQuestion2 = true;
            tranTThree[1].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[1].SetActive(false);
            HQuestion2 = false;
            tranTThree[1].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ3()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion3)
        {
            TypeThreeA[2].SetActive(true);
            HQuestion3 = true;
            tranTThree[2].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[2].SetActive(false);
            HQuestion3 = false;
            tranTThree[2].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ4()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion4)
        {
            TypeThreeA[3].SetActive(true);
            HQuestion4 = true;
            tranTThree[3].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[3].SetActive(false);
            HQuestion4 = false;
            tranTThree[3].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ5()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion5)
        {
            TypeThreeA[4].SetActive(true);
            HQuestion5 = true;
            tranTThree[4].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[4].SetActive(false);
            HQuestion5 = false;
            tranTThree[4].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ6()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion6)
        {
            TypeThreeA[5].SetActive(true);
            HQuestion6 = true;
            tranTThree[5].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[5].SetActive(false);
            HQuestion6 = false;
            tranTThree[5].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ7()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion7)
        {
            TypeThreeA[6].SetActive(true);
            HQuestion7 = true;
            tranTThree[6].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[6].SetActive(false);
            HQuestion7 = false;
            tranTThree[6].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ8()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion8)
        {
            TypeThreeA[7].SetActive(true);
            HQuestion8 = true;
            tranTThree[7].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[7].SetActive(false);
            HQuestion8 = false;
            tranTThree[7].rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void TypeThreeQ9()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        if (!HQuestion9)
        {
            TypeThreeA[8].SetActive(true);
            HQuestion9 = true;
            tranTThree[8].rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            TypeThreeA[8].SetActive(false);
            HQuestion9 = false;
            tranTThree[8].rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}

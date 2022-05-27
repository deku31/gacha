using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum stat{ mulai, sudahmulai, result
}
public class gamePlayManager : MonoBehaviour
{
    [Header("Money")]
    public float money=100000;//uang kita
    public float[] moneyDraw;//minimal uang agar dapat draw. jika 0 draw normal jika 1 draw super

    public Text moneyTxt;
    public Text PrizenormalTxt;
    public Text PrizeSuperTxt;

    public Text statusPrize;

    public stat status = stat.result;//stat.mulai berarti slot siap dimulai stat.sudahmulai berarti slot blm siap dimainkan
    [SerializeField] private GameObject[] PilihItemBtn;// button pilih item
    /* catatan: 
     gameobject 0= gold
     gameobject 1= silver
     gameobject 2= zonk*/
    [SerializeField] private GameObject[] jenisItem;

    [Header("Total Item")]
    [SerializeField] private int TotGold=3;
    [SerializeField] private int TotSilver=3;
    [SerializeField] private int TotZonk=3;

    public bool mulaiSlot;//jika true item slot akan tersembunyi jika false akan memunculkan item slot
    public bool superPrize;

    int acak1;
    GameObject childObj;
    private void Awake()
    {
        PrizenormalTxt.text ="Rp."+ moneyDraw[0].ToString();
        PrizeSuperTxt.text ="Rp."+ moneyDraw[1].ToString();
    }
    void Start()
    {
        restart();
    }
    
    void Update()
    {
        if (superPrize==false)
        {
            statusPrize.text = "Normal Prize";

        }
        else
        {
            statusPrize.text = "Super Prize";

        }
        moneyTxt.text ="Rp."+ money.ToString();
        if (mulaiSlot==true)
        {
            restart();
            if (money<moneyDraw[1])
            {
                superPrize = false;
            }
            mulaiSlot = false;
        }
    }
    public void restart()//untuk mereset game
    {
        foreach (var item in jenisItem)
        {
            item.active = false;
        }
        Acak();
    }
    public void Acak()//fungsi acak memunculkan item
    {
        for (int i = 0; i < PilihItemBtn.Length; i++)
        {
            acak1 = Random.RandomRange(0, jenisItem.Length);
            Instantiate(jenisItem[acak1], PilihItemBtn[i].transform);
        }

    }
    IEnumerator destroyedItem()//memberikan waktu 0.5 detik unuk menghilangkan item pada slot
    {
        foreach (var item in PilihItemBtn)
        {
            childObj = item.transform.GetChild(0).gameObject;
            yield return new WaitForSeconds(0.1f);
            DestroyObject(childObj);
        }
        mulaiSlot = true;
        status = stat.result;
        StopCoroutine(destroyedItem());
    }
    IEnumerator instanceObj()// berfungsi untuk menspawn object dalam waktu 0.2 detik
    {
        yield return new WaitForSecondsRealtime(0.8f);
        StartCoroutine(openItem());
        StopCoroutine(instanceObj());
    }
    IEnumerator openItem()//membuka semua item sesuai angka yang ada pada daftar item
    {
        for (int i = 0; i < PilihItemBtn.Length; i++)
        {
            childObj = PilihItemBtn[i].transform.GetChild(0).gameObject;
            childObj.active = true;
            yield return new WaitForSeconds(0.1f);
        }
        status = stat.mulai;
       
        
        StopCoroutine(openItem());
    }
    public void pilihItem(int noitem)// untuk menentukan button pada ui menginput nomor berapa
    {
        if (status==stat.result)
        {
            childObj = PilihItemBtn[noitem].transform.GetChild(0).gameObject;
            childObj.active = true;
            money+=childObj.GetComponent<itemScript>().prizeTxt;
            status = stat.sudahmulai;
            StartCoroutine(instanceObj());
        }
       
    }
    public void btnStar()//script untuk tombol start
    {
        if (money>=moneyDraw[0])
        {
            if (status == stat.mulai)
            {
                mulaiSlot = true;
                status = stat.sudahmulai;
                StartCoroutine(destroyedItem());
                restart();
                if (superPrize==false)//normal prize
                {
                    money -= moneyDraw[0];
                }
                else
                {
                    money -= moneyDraw[1];
                }
            }
        }
    }
    public void normalbtnPrize()//tombol hadiah normal
    {
        superPrize = false;
    }
    public void superBtnPrize()//tombol hadiah super
    {
        if (money>moneyDraw[1])
        {
            superPrize = true;
        }
    }
}

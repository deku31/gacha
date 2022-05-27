using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemScript : MonoBehaviour
{
    [SerializeField] private gamePlayManager gpm;//memanggil game pay manager
    public int prizeTxt;//text uang yang ada di prefabs
    [SerializeField] private Text prizeObj;
    /*catatan : 
     0 = normal
     1= super
     */
    public int[] prizeValue;
    
    public void Awake()
    {
        gpm = FindObjectOfType<gamePlayManager>();//mencari game play manager
        manager();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void manager()
    {
        if (gpm.superPrize == true)
        {
            prizeTxt = prizeValue[1];
        }
        else
        {
            prizeTxt = prizeValue[0];

        }
        prizeObj.text = "Rp." + prizeTxt.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        manager();
        if (gpm.mulaiSlot==true)
        {
            Destroy(this.gameObject);
        }
    }
}

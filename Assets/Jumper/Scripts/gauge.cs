using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gauge : MonoBehaviour
{
    public GameObject[] Gauges;
    
    private int NumofFireballDestroyed;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        // SwordBehaviour에서 NumberOfFireballDestoyed int값 가져옴.
        //NumofFireballDestroyed = WeaponSword.gameObject.GetComponent<SwordBehaviour>().NumberOfFireballDestroyed;
    }

    public void GaugeUpdate(int gaugesIdx)
    {
        Gauges[gaugesIdx - 1].gameObject.GetComponent<Image>().color = Color.red;

    }


    public void ResetGauge()
    {
        /*
        for(int i = 0; i < 5; i++)
        {
            Gauges[i].gameObject.GetComponent<Image>().color = Color.white;
        }
        */
        StartCoroutine(GaugeReset());
    }

    IEnumerator GaugeReset()
    {
        int MaxGauge = 5;

        while(MaxGauge-- !=0) {
            Gauges[MaxGauge].GetComponent<Image>().color = Color.white;
            
            StartCoroutine(Waitfor());
            yield return null;
        }
        
    }

    IEnumerator Waitfor()
    {
        yield return new WaitForSeconds(100f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreYouSureBoxManager : MonoBehaviour
{    

    public void OnClickAreYouSureBoxBackBtn()
    {
        GetComponent<Animator>().SetTrigger("popout");
        StartCoroutine(UnactiveBox());

    }

    private IEnumerator UnactiveBox()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        yield return null;
    }
}

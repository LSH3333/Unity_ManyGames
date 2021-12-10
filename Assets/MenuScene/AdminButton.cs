using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminButton : MonoBehaviour
{
    public void OnClickBtn()
    {
        InstaniateAdminPanel();
    }

    private void InstaniateAdminPanel()
    {
        GameObject resource = (GameObject)Resources.Load("AdminBoard");
        GameObject obj = Instantiate(resource, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(GameObject.Find("Canvas").transform);

        ((RectTransform)obj.transform).anchoredPosition = new Vector2(0, 0);

    }
}

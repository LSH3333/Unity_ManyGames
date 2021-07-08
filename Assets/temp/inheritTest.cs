
using UnityEngine;

public class inheritTest : MonoBehaviour
{

    private void Start()
    {
        //Debug.Log(GameObject.Find("Child1").GetComponent<parent>().GetType().Name);
        //Debug.Log(GameObject.Find("Child2").GetComponent<parent>().GetType().Name);

        parent a = GameObject.Find("Child1").GetComponent<parent>();
        child1 b = GameObject.Find("Child1").GetComponent<child1>();
        Debug.Log(a);
        
    }
}

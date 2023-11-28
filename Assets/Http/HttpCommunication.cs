using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpCommunication : MonoBehaviour
{

    private void Start()
    {
        //StartCoroutine(UnityWebRequestGet());
        StartCoroutine(UnityWebRequestPost());
    }

    IEnumerator UnityWebRequestGet()
    {
        string url = "http://localhost:8080/tmp";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest(); 

        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("ERROR");
        }
        
    }

    IEnumerator UnityWebRequestPost()
    {
        string url = "http://localhost:8080/member/login";

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("name=jake&password=1234"));
        formData.Add(new MultipartFormDataSection("name", "jake"));
        formData.Add(new MultipartFormDataSection("password", "1234"));

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, formData);
        yield return unityWebRequest.SendWebRequest();

        if(unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.Log(unityWebRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete");
            string response = unityWebRequest.downloadHandler.text;
            Debug.Log("response = " + response);
        }
    }

}

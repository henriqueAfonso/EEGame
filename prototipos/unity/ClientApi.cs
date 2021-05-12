using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;

public class ClientApi : MonoBehaviour
{
    public string url;
    public Controls controls;

    void Start()
    {
        StartCoroutine(Get(url));
    }

    public IEnumerator Get(string url)
    {
    	while(true){
        	using(UnityWebRequest www = UnityWebRequest.Get(url)){
            	yield return www.SendWebRequest();

            	if (www.result == UnityWebRequest.Result.ConnectionError)
            	{
            	    Debug.Log(www.error);
            	}
            	else
            	{
                	if (www.isDone)
                	{
                	    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                	    //Debug.Log(result);
                	    controls = JsonUtility.FromJson<Controls>(result);
                	    //Debug.Log("Jump_Down: " + controls.jumpDown + ", Jump_Up: " + controls.jumpUp + ", Horizontal: " + controls.horizontal);
                	}
                	else
            	    {
                    	Debug.Log("Error! data couldn't get.");
                	}
            	}
        	}
        	Thread.Sleep(1);
        }
    }
}

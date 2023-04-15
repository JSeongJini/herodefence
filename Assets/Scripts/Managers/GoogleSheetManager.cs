using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkData
{
    public string result;
    public string msg;
    public int value;

    public NetworkData()
    {
        result = "waiting";
        msg = "";
        value = -1;
    }
}


public class GoogleSheetManager : Singlton<GoogleSheetManager>
{
    private static string URL = "https://script.google.com/macros/s/AKfycbybnFdLOd0gKT178_5OU8zBffQl677GPxI19HTiG7Kn620DHCwQJOXu39fYpVZTUKzd/exec";

    public IEnumerator Post(WWWForm _form, NetworkData _data)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, _form))
        {
            yield return www.SendWebRequest();

            if (www.isDone)
            {
                NetworkData data = JsonUtility.FromJson<NetworkData>(www.downloadHandler.text);
                _data.result = data.result;
                _data.msg = data.msg;
                _data.value = data.value;
            }else if (www.result == UnityWebRequest.Result.ConnectionError)
            {

            }
        }
    }
}

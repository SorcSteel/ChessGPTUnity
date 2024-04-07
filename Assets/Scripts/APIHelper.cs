using System.Net;
using  System.IO;
using UnityEngine;

public static class APIHelper
{
    public static User GetUser(string username, string password)
    {
        HttpWebRequest request =(HttpWebRequest)WebRequest.Create(GetApiUrl(username, password));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<User>(json);
    }

    public static string GetApiUrl(string username, string password)
    {
        string url = "https://localhost:7230/api/User/"+ username + "/" + password;
        return url;
    }
}
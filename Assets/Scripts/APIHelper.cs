using System.Net;
using  System.IO;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public static class APIHelper
{
    public static string baseUrl = "https://bigprojectapi-500201348.azurewebsites.net/api/";
    public static IEnumerator Login(string username, string password, Action<User> onSuccess, Action<string> onError)
    {
        string url = baseUrl + "User/" + UnityWebRequest.EscapeURL(username) + "/" + UnityWebRequest.EscapeURL(password);

        // Send the request
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            // Check for errors
            if (www.isNetworkError || www.isHttpError)
            {
                onError?.Invoke("Failed to login. Error: " + www.error);
            }
            else
            {
                // Parse response
                string json = www.downloadHandler.text;
                User user = JsonUtility.FromJson<User>(json);
                onSuccess?.Invoke(user);
            }
        }
    }

public static int CreateAccount(User user)
{    
   try
   {
     // Create JSON payload
     string jsonPayload = JsonUtility.ToJson(user);
 
     // Create HttpWebRequest
     HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + "User/false");
     request.Method = "POST";
     request.ContentType = "application/json";
 
     // Write JSON payload to request stream
     using (var streamWriter = new StreamWriter(request.GetRequestStream()))
     {
         streamWriter.Write(jsonPayload);
         streamWriter.Flush();
         streamWriter.Close();
     }
 
     // Get response and deserialize JSON
     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
     StreamReader reader = new StreamReader(response.GetResponseStream());
     return 1;
   }
   catch (System.Exception)
   {
    return 0;
   }
}
}
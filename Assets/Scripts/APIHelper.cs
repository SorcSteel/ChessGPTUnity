using System.Net;
using  System.IO;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Threading.Tasks;

public static class APIHelper
{
    public static string baseUrl = "https://bigprojectapi-500201348.azurewebsites.net/api/";
    public static IEnumerator Login(string username, string password, Action<User> onSuccess, Action<string> onError)
    {
        string url = baseUrl + "User/" + UnityWebRequest.EscapeURL(username) + "/" + UnityWebRequest.EscapeURL(password);

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                onError?.Invoke("Failed to login. Error: " + www.error);
            }
            else
            {
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
     string jsonPayload = JsonUtility.ToJson(user);
 
     HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseUrl + "User/false");
     request.Method = "POST";
     request.ContentType = "application/json";
 
     using (var streamWriter = new StreamWriter(request.GetRequestStream()))
     {
         streamWriter.Write(jsonPayload);
         streamWriter.Flush();
         streamWriter.Close();
     }
 
     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
     StreamReader reader = new StreamReader(response.GetResponseStream());
     return 1;
   }
   catch (System.Exception)
   {
    return 0;
   }
}

public static string GetAIMove(string board)
    {
        string url = baseUrl + "OpenAI/ComputerMove?board=" + UnityWebRequest.EscapeURL(board);

        // Create the request
        WebRequest request = WebRequest.Create(url);
        request.Method = "POST";

        try
        {
            // Get the response
            WebResponse response = request.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            {
                // Read the response and return it
                string result = reader.ReadToEnd();
                return result;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error calling API: " + e.Message);
            return null;
        }
    }
}
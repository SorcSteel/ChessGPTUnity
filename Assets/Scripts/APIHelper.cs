using System.Net;
using  System.IO;
using UnityEngine;
using System;
using TMPro;

public static class APIHelper
{
    public static string baseUrl = "https://localhost:7230/api/";
    public static User Login(string username, string password)
    {
        HttpWebRequest request =(HttpWebRequest)WebRequest.Create(baseUrl + "User/" + username + "/" + password);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        //GameObject.FindGameObjectWithTag("txtUsername").GetComponent<TMP_InputField>().text = json;
        return JsonUtility.FromJson<User>(json);
    }

public static int CreateAccount(User user)
{    
   try
   {
     // Create JSON payload
     string jsonPayload = JsonUtility.ToJson(user);
 
     // Create HttpWebRequest
     HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:7230/api/User/false");
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
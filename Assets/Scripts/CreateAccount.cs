using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Linq;
using SimpleJSON;

public class CreateAccount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("txtPassword").GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Password;
    }

    public void btnCreateAccountClicked()
    {
        string firstName = GameObject.FindGameObjectWithTag("txtFirstName").GetComponent<TMP_InputField>().text;
        string lastName = GameObject.FindGameObjectWithTag("txtLastName").GetComponent<TMP_InputField>().text;
        string username = GameObject.FindGameObjectWithTag("txtUsername").GetComponent<TMP_InputField>().text;
        string password = GameObject.FindGameObjectWithTag("txtPassword").GetComponent<TMP_InputField>().text;

        User user = new User
        {
         Id = Guid.Empty,
         UserName = username,
         Password = password,
         FirstName = firstName,
         LastName = lastName,
         IsComputer = false
        };

         int result = APIHelper.CreateAccount(user);

         if(result == 1)
         {
            //worked
            GameObject.FindGameObjectWithTag("txtResult").GetComponent<TMP_Text>().text = "Successfully Created Account";
         }
        else
        {
            GameObject.FindGameObjectWithTag("txtResult").GetComponent<TMP_Text>().text = "Failed Creating Account";
        }
    }
    public void btnGoBackClicked()
    {
        SceneManager.LoadScene("LoginPage");
    }
}

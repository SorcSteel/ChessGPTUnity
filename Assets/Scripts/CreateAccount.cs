using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
         id = Guid.Empty,
         userName = username,
         password = password,
         firstName = firstName,
         lastName = lastName,
         isComputer = false
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

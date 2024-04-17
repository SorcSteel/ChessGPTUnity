using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
   void Start () {
      GameObject.FindGameObjectWithTag("txtPassword").GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Password;
   }

   public void btnLoginClicked()
{
    string username = GameObject.FindGameObjectWithTag("txtUsername").GetComponent<TMP_InputField>().text;
    string password = GameObject.FindGameObjectWithTag("txtPassword").GetComponent<TMP_InputField>().text;

    StartCoroutine(APIHelper.Login(username, password, OnLoginSuccess, OnLoginError));
}

private void OnLoginSuccess(User user)
{
    if (user.firstName == "")
    {
        // Invalid user
        GameObject.FindGameObjectWithTag("txtUsername").GetComponent<TMP_InputField>().text = "Invalid";
    }
    else
    {
        // Valid user, proceed to main menu
        SceneManager.LoadScene("MainMenu");
    }
}

private void OnLoginError(string errorMessage)
{
    // Handle login error
    Debug.LogError("Login error: " + errorMessage);
}

   public void btnRegisterClicked()
   {
      SceneManager.LoadScene("CreateAccountPage");
   }
}
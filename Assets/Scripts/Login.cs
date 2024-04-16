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

      User user = APIHelper.Login(username, password);


      if (user.firstName == "")
      {
         //invalid user
         GameObject.FindGameObjectWithTag("txtUsername").GetComponent<TMP_InputField>().text = "Invalid";
      }
      else
     {
         SceneManager.LoadScene("MainMenu");
      }
   }

   public void btnRegisterClicked()
   {
      SceneManager.LoadScene("CreateAccountPage");
   }
}
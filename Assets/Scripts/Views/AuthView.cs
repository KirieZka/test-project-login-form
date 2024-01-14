using UnityEngine;
using UnityEngine.UI;

public class AuthView : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Text errorText;

    private AuthController authController;

    private void Start()
    {
        authController = new AuthController(this, new AuthModel());
    }

    public void OnLoginButtonClick()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Вызываем метод TryAuthenticate при нажатии кнопки
        authController.TryAuthenticate(username, password);
    }

    public void DisplayError(string errorMessage)
    {
        errorText.text = errorMessage;
    }
}

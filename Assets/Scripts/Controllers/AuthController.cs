using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class AuthController: MonoBehaviour
{
    private readonly AuthView authView;
    private readonly AuthModel authModel;

    public AuthController(AuthView view, AuthModel model)
    {
        authView = view;
        authModel = model;
    }

    public void TryAuthenticate(string username, string password)
    {
        UniTask.Void(async () =>
        {
            try
            {
                string accessToken = await authModel.AuthenticateAsync(username, password);

                // Используйте UnityMainThreadDispatcher для выполнения кода в главном потоке
                await UniTask.SwitchToMainThread();
                Debug.Log("Authentication successful. Access Token: " + accessToken);
            }
            catch (Exception ex)
            {
                Debug.LogError("Authentication failed: " + ex.Message);

                // Используйте UnityMainThreadDispatcher для выполнения кода в главном потоке
                await UniTask.SwitchToMainThread();
                authView.DisplayError("Authentication failed. Please check your credentials.");
            }
        });
    }
}

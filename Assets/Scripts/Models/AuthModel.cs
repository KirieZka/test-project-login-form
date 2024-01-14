using Cysharp.Threading.Tasks;
using System.Net.Http;
using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class AuthModel: MonoBehaviour
{
    private readonly string apiUrl = "https://stage.arenagames.api.ldtc.space/";
    private readonly string loginEndpoint = "api/v3/gamedev/client/auth/sign-in";

    public async UniTask<string> AuthenticateAsync(string username, string password)
    {
        // Создаем объект с данными для отправки на сервер
        var requestData = new RequestData
        {
            login = username,
            password = password
        };

        try
        {
            // Отправляем запрос на сервер
            var response = await ApiClient.PostAsync(apiUrl + loginEndpoint, requestData);

            // Обрабатываем ответ от сервера и извлекаем токен
            var token = ParseResponse(response);

            return token;
        }
        catch (HttpRequestException ex)
        {
            // Обработка ошибок запроса
            throw new HttpRequestException($"Authentication request failed: {ex.Message}");
        }
    }


    private string ParseResponse(string response)
    {
        try
        {
            // Разбираем JSON-ответ, чтобы получить токен
            var responseData = JsonConvert.DeserializeObject<ResponseData>(response);

            // Выводим токен в консоль через LogError
            Debug.Log($"Access Token: {responseData.accessToken.token}");

            return responseData.accessToken.token;
        }
        catch (Exception ex)
        {
            // Обработка ошибок парсинга
            Debug.LogError($"Error parsing response: {ex.Message}");
            throw;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
using System.Net.Http;
using System.Text;

public class ApiClient
{
    public static async UniTask<string> PostAsync(string url, object requestData)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(requestData));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        var operation = request.SendWebRequest();

        // Ждем завершения операции UnityWebRequest
        await operation;

        // Проверяем наличие ошибок
        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new HttpRequestException($"Failed to send request. Error: {request.error}");
        }

        return request.downloadHandler.text;
    }
}

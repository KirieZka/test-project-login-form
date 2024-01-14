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
        // ������� ������ � ������� ��� �������� �� ������
        var requestData = new RequestData
        {
            login = username,
            password = password
        };

        try
        {
            // ���������� ������ �� ������
            var response = await ApiClient.PostAsync(apiUrl + loginEndpoint, requestData);

            // ������������ ����� �� ������� � ��������� �����
            var token = ParseResponse(response);

            return token;
        }
        catch (HttpRequestException ex)
        {
            // ��������� ������ �������
            throw new HttpRequestException($"Authentication request failed: {ex.Message}");
        }
    }


    private string ParseResponse(string response)
    {
        try
        {
            // ��������� JSON-�����, ����� �������� �����
            var responseData = JsonConvert.DeserializeObject<ResponseData>(response);

            // ������� ����� � ������� ����� LogError
            Debug.Log($"Access Token: {responseData.accessToken.token}");

            return responseData.accessToken.token;
        }
        catch (Exception ex)
        {
            // ��������� ������ ��������
            Debug.LogError($"Error parsing response: {ex.Message}");
            throw;
        }
    }
}

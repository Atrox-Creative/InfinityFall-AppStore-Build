using UnityEngine;
using System.Net;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using System.Threading.Tasks;
using System;

public static class APIHandler
{
    public static async Task<Player> AddPlayer()
    {
        string response = await Post("/player", "", "");
        Player player = JsonUtility.FromJson<Player>(response);
        return player;
    }
    public static async Task<Player> UpdatePlayer(string newName, string token)
    {
        Debug.Log("Update Player");
        string response = await Put("/player", "{\"name\": \"" + newName + "\"}", token);
        Player player = JsonUtility.FromJson<Player>(response);
        return player;
    }
    public static async Task<Score> AddScore(int value, float duration, string token)
    {
        SendScore sendScore = new SendScore();
        sendScore.value = value;
        sendScore.duration = duration;
        string json = JsonUtility.ToJson(sendScore);

        string response = await Post("/score", json, token);
        return JsonUtility.FromJson<Score>(response);
    }
    public static async Task<Score> GetBestScorePlayer(string token)
    {
        string response = await Get("/score/best", token);
        return JsonUtility.FromJson<Score>(response);
    }
    public static async Task<Score[]> GetScores(int skip, int limit)
    {
        string response = await Get("/score?skip=" + skip.ToString() + "&limit=" + limit.ToString(), "");
        string other = "{\"Items\":" + response + "}";
        Score[] scores = JsonHelper.FromJson<Score>(other);
        return scores;
    }

    private static async Task<string> Get(string url, string token)
    {
        string domain = "https://infinity-fall.up.railway.app";

        using var request = UnityWebRequest.Get(domain + url);
        return await HandleRequest(request, token);
    }

    private static async Task<string> Post(string url, string postData, string token)
    {
        string domain = "https://infinity-fall.up.railway.app";

        using var request = UnityWebRequest.Put(domain + url, postData);
        request.method = UnityWebRequest.kHttpVerbPOST;

        return await HandleRequest(request, token);
    }
    private static async Task<string> Put(string url, string bodyData , string token)
    {
        string domain = "https://infinity-fall.up.railway.app";

        using var request = UnityWebRequest.Put(domain + url, bodyData);
        return await HandleRequest(request, token);
    }

    private static async Task<string> HandleRequest(UnityWebRequest request, string token)
    {
        request.timeout = 10;
        request.SetRequestHeader("Content-Type", "application/json");
        if (token.Length > 0) request.SetRequestHeader("Authorization", "Bearer " + token);

        var operation = request.SendWebRequest();

        while (!operation.isDone) await Task.Yield();


        if (request.result == UnityWebRequest.Result.Success)
        {
            return request.downloadHandler.text;
        }
        else
        {
            string response = request.downloadHandler != null ? request.downloadHandler.text : string.Empty;
            throw new Exception("HTTP " + request.responseCode + ": " + request.error + " " + response);
        }
    }
}

[Serializable]
public class SendScore
{
    public int value;
    public float duration;
    public int maxCombo = 1;
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

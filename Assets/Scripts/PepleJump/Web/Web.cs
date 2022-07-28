using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Text;
using TMPro;

public class Web
{
    [System.Serializable]
    public class Id
    {
        [SerializeField] public int id;
    }
    [System.Serializable]
    public class Score
    {
        [SerializeField] public int score;
    }

    public int game_id;

    public IEnumerator InitGame()
    {
        WWWForm form = new WWWForm();
        string chat_id;

        var parm = ParamParse.GetBrowserParameters();

        if (parm == null) yield break;

        parm.TryGetValue("chat_id", out chat_id);

        form.AddField("chat_id", chat_id);

        using (UnityWebRequest www = UnityWebRequest.Post("https://punk-verse.thesmartnik.com/platformer_games", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");

                string rawJson = Encoding.Default.GetString(www.downloadHandler.data);

                game_id = JsonUtility.FromJson<Id>(rawJson).id;
            }
        }
    }

    public IEnumerator EndGame(int score)
    {
        Score score1= new Score();
        score1.score = score;
        string result = JsonUtility.ToJson(score1);

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(result);

        using (UnityWebRequest www = UnityWebRequest.Put("https://punk-verse.thesmartnik.com/platformer_games/" + game_id, myData))
        {
            www.uploadHandler.contentType = "application/json";

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}

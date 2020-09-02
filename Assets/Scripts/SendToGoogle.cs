using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SendToGoogle : MonoBehaviour
{
    public GameObject funSlider;

    private string funResponse;
    private string mapLength;

    [SerializeField]
    private string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfv-wF6fxOtg8FxWHhNYNGu2iDyF45KhsHN4zuc_K3Bnc5k9Q/formResponse";

    IEnumerator Post(string funResponse)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.218428610", funResponse);
        form.AddField("entry.1161026783", mapLength);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        SceneManager.LoadScene(0);
    }

    public void Send()
    {
        funResponse = funSlider.GetComponent<Slider>().value.ToString();
        mapLength = PlayerPrefs.GetInt("numTiles").ToString();
        Debug.Log("this map was " + mapLength + " tiles long");
        StartCoroutine(Post(funResponse));
    }

}

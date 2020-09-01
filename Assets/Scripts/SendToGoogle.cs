using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour
{
    public GameObject funSlider;
    public GameObject mapGenerator;

    private string funResponse;
    private string mapLength;

    [SerializeField]
    private string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfv-wF6fxOtg8FxWHhNYNGu2iDyF45KhsHN4zuc_K3Bnc5k9Q/formResponse";

    IEnumerator Post(string funResponse)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.218428610", funResponse);
        byte[] rawData = form.data;
        WWW www = new WWW(url, rawData);
        yield return www;
    }

    public void Send()
    {
        funResponse = funSlider.GetComponent<Slider>().value.ToString();
        StartCoroutine(Post(funResponse));
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Image=UnityEngine.UI.Image;



public class PlayerUI : MonoBehaviour
{
    Image Healtbar;
    float tmphealth;


    private void Start()
    {
        Healtbar = GameObject.Find("PlayerUI").transform.FindChild("Bar").FindChild("Health").GetComponent<Image>();
    }


    void Update()
    {
        tmphealth = GetComponent<Player>().GetHealthPct();
        Healtbar.fillAmount = tmphealth;

    }

}

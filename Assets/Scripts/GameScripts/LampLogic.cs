using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampLogic : MonoBehaviour
{
    public void LightsOut()
    {
        StartCoroutine(LightsOutAndOn());
    }

    private IEnumerator LightsOutAndOn()
    {
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.lampTurnOff);
        GetComponent<Light2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Light2D>().enabled = true;
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.lampTurnOn);
    }
}

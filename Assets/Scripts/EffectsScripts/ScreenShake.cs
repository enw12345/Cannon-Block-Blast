using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Camera))]
public class ScreenShake : MonoBehaviour
{
    private float timeElapsed = 0;

    public void Shake(float shakeTime)
    {
        timeElapsed = 0;

        while (timeElapsed < shakeTime)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x + Random.value,
                transform.localPosition.y + Random.value,
                transform.localPosition.z + Random.value) * 0.3f;

            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);
        }
    }

    public IEnumerator ShakeOverTime(float shakeTime)
    {
        timeElapsed = 0;

        while (timeElapsed < shakeTime)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x + Random.value,
                transform.localPosition.y + Random.value,
                transform.localPosition.z + Random.value) * 0.3f;

            timeElapsed += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
            Debug.Log(timeElapsed);
        }
    }
}

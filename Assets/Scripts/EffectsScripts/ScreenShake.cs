using System.Collections;
using UnityEngine;

namespace EffectsScripts
{
    [RequireComponent(typeof(Camera))]
    public class ScreenShake : MonoBehaviour
    {
        private float timeElapsed;

        public void Shake(float shakeTime)
        {
            timeElapsed = 0;

            while (timeElapsed < shakeTime)
            {
                var localPosition = transform.localPosition;
                localPosition = new Vector3(
                    localPosition.x + Random.value,
                    localPosition.y + Random.value,
                    localPosition.z + Random.value) * 0.3f;
                transform.localPosition = localPosition;

                timeElapsed += Time.deltaTime;
                Debug.Log(timeElapsed);
            }
        }

        public IEnumerator ShakeOverTime(float shakeTime)
        {
            timeElapsed = 0;

            while (timeElapsed < shakeTime)
            {
                var localPosition = transform.localPosition;
                localPosition = new Vector3(
                    localPosition.x + Random.value,
                    localPosition.y + Random.value,
                    localPosition.z + Random.value) * 0.3f;
                transform.localPosition = localPosition;

                timeElapsed += Time.deltaTime;
                yield return new WaitForSeconds(0.01f);
                Debug.Log(timeElapsed);
            }
        }
    }
}
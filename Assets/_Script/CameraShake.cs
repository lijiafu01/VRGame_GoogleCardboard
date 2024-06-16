using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    
    public IEnumerator Shake(float duration, float initialMagnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        float currentMagnitude = initialMagnitude;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            // Giảm dần độ lớn của rung dựa trên thời gian
            currentMagnitude = Mathf.Lerp(initialMagnitude, 0, elapsed / duration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

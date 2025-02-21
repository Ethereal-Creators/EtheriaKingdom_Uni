using UnityEngine;
using System.Collections;

public class ShieldScaler : MonoBehaviour
{
    public float scaleDuration = 1f; // Time to reach full scale
    public Vector3 targetScale = new Vector3(5f, 5f, 5f); // Final scale of the shield
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 0.1f); // Initial scale of the shield

    private void Start()
    {
        // Set the initial scale and start the coroutine to scale the shield
        transform.localScale = initialScale;
        StartCoroutine(ScaleShieldIn());
    }

    // Coroutine to smoothly scale the shield to the target size
    private IEnumerator ScaleShieldIn()
    {
        float timeElapsed = 0f;

        // Gradually scale the shield from the initial scale to the target scale
        while (timeElapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / scaleDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is exactly the target scale
        transform.localScale = targetScale;
    }
}

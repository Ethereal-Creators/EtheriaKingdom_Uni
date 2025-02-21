using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Don't forget to include UnityEngine.UI for Image and Text components!

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;
    [SerializeField]
    private UnityEngine.UI.Text _healthText; // Text to show the health percentage
    [SerializeField]
    private Color _lowHealthColor = Color.red; // Color for low health
    [SerializeField]
    private Color _mediumHealthColor = Color.yellow; // Color for medium health
    private float _lerpSpeed = 0.05f;

    private float _targetHealthPercentage;

    private void Start()
    {
        _targetHealthPercentage = _healthBarForegroundImage.fillAmount; // Initialize the target health
    }

    public void UpdateHealthBar(HealthController healthController)
    {
        // Get the new health percentage from the health controller
        float newHealthPercentage = healthController.RemainingHealthPercentage;

        // Update text showing current health percentage (optional)
        if (_healthText != null)
        {
            _healthText.text = Mathf.RoundToInt(newHealthPercentage * 100) + "%"; // Show percentage
        }

        // Smoothly transition the health bar value to the new percentage
        StartCoroutine(SmoothHealthUpdate(newHealthPercentage));

        // Change color based on health
        UpdateHealthBarColor(newHealthPercentage);
    }
    
    private IEnumerator SmoothHealthUpdate(float target)
    {
        // Smoothly transition towards the target health value
        while (Mathf.Abs(_healthBarForegroundImage.fillAmount - target) > 0.01f)
        {
            _healthBarForegroundImage.fillAmount = Mathf.Lerp(_healthBarForegroundImage.fillAmount, target, _lerpSpeed);
            yield return null; // Wait for the next frame
        }

        // Ensure we reach the exact target value
        _healthBarForegroundImage.fillAmount = target;
    }

    private void UpdateHealthBarColor(float healthPercentage)
    {
 
        if (healthPercentage > 0.3f) // Medium health
        {
            _healthBarForegroundImage.color = _mediumHealthColor;
        }
        else // Low health
        {
            _healthBarForegroundImage.color = _lowHealthColor;
            // Optional: You could add a flashing effect here if health is below a certain threshold
        }
    }
}

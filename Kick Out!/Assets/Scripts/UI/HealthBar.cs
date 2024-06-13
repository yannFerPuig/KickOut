using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // COMPONENTS
    public Slider slider;
    public Image fillImage; // Reference to the Image component of the fill area
    public Color normalColor = Color.green; // Normal color of the health 
    public Color decreasingColor = Color.red; // Color when the health is decreasing
    private Coroutine healthCoroutine;

    // Set the health bar to the max value and initialize the color
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fillImage.color = normalColor; // Set the initial color to normal
    }

    // Whenever the health of the character is updated, the health bar is also updated smoothly
    public void SetHealth(float health)
    {
        // If the current coroutine is running, stop it
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
        }

        // Check if the health is 0, and reset to max if so
        if (health <= 0)
        {
            slider.value = slider.maxValue;
            fillImage.color = normalColor;
        }
        else if (health == slider.maxValue)
        {
            // Immediately update the health bar if health is being set to max value
            slider.value = health;
            fillImage.color = normalColor;
        }
        else
        {
            // Start a new coroutine to smoothly change the health value
            healthCoroutine = StartCoroutine(SmoothHealthChange(health));
        }
    }

    // Coroutine to smoothly change the health value
    private IEnumerator SmoothHealthChange(float newHealth)
    {
        float currentHealth = slider.value;
        float duration = 0.3f;
        float elapsed = 0f;

        // Set the color based on whether the health is decreasing
        fillImage.color = (newHealth < currentHealth) ? decreasingColor : normalColor;

        while (elapsed < duration)
        {
            slider.value = Mathf.Lerp(currentHealth, newHealth, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final value and color are correctly set
        slider.value = newHealth;
        fillImage.color = normalColor;

        // Check if the health is 0 after the transition and reset to max if so
        if (slider.value <= 0)
        {
            slider.value = slider.maxValue;
            fillImage.color = normalColor;
        }
    }
}

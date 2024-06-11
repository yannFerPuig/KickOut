using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // COMPONENTS
    public Slider slider;
    public Image fillImage; // Reference to the Image component of the fill area
    public Color normalColor = Color.red; // Normal color of the health 
    public Color decreasingColor = Color.yellow; // Color when the health is decreasing
    private Coroutine healthCoroutine;

    void Start()
    {
        
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fillImage.color = normalColor;
    }

    // Whenever the health of the character is updated, the health bar is also updated smoothly
    public void SetHealth(float health)
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
        }
        healthCoroutine = StartCoroutine(SmoothHealthChange(health));
    }

    // Coroutine to smoothly change the health value
    private IEnumerator SmoothHealthChange(float newHealth)
    {
        float currentHealth = slider.value;
        float duration = 0.5f; 
        float elapsed = 0f;

        if (newHealth < currentHealth)
        {
            fillImage.color = decreasingColor;
        }
        else
        {
            fillImage.color = normalColor;
        }

        while (elapsed < duration)
        {
            slider.value = Mathf.Lerp(currentHealth, newHealth, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        slider.value = newHealth; // final value is set
        fillImage.color = normalColor; // Reset to normal
    }
}

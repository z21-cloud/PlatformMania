using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class HealthView : MonoBehaviour, IHealthView
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private string prefix = "Health: ";

    public void UpdateHealth(float current)
    {
        if (healthText.text != null)
            healthText.text = $"{prefix}{current}";
    }
}

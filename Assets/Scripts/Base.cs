using System;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int baseHealth = 30;
    [SerializeField] private TextMeshProUGUI health;

    private void Start()
    {
        UpdateHealth();
    }

    public void DamageBase(int damage)
    {
        baseHealth -= damage;
        UpdateHealth();
        if (baseHealth <= 0)
        {
            Debug.Log("Game Over :(");
        }
    }

    private void UpdateHealth()
    {
       health.SetText($"({baseHealth}] [Health)"); 
    }
}

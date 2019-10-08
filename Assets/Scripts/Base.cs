using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int baseHealth = 30;

    public void DamageBase(int damage)
    {
        baseHealth -= damage;
        if (baseHealth <= 0)
        {
            Debug.Log("Game Over :(");
        }
    }
}

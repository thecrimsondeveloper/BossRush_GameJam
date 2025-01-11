using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Weapon currentWeapon;
    public int health = 12;
    public VisualEffect playerGUIVisuals;

    public UnityEvent OnDie = new UnityEvent();

    void Start()
    {
        playerGUIVisuals.SetInt("Health", health);
        playerGUIVisuals.SetInt("Max Health", health);
    }   

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        playerGUIVisuals.SetInt("Health", health);
    }

    private void Die()
    {
        OnDie.Invoke();
        Debug.Log("Player died");
    }
}

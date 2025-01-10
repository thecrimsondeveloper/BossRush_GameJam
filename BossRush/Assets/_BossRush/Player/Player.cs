using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Weapon currentWeapon;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Attack();
        }
    }
}

using UnityEngine;
using UnityEngine.VFX;

public class LaserRifle : Weapon
{
    public LayerMask shootableLayer;
    public Transform firePoint;
    public Animation fireAnimation;
    public VisualEffect bullet;


    protected override void TryAttack()
    {
        //raycast to detect enemies
        RaycastHit hit;

        bullet.Play();

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100, shootableLayer))
        {
            Debug.Log(hit.transform.name);
        }
    }

    protected override void WhenUsed(Player attacker)
    {   
        TryAttack();
        // fireAnimation.Play();
    }

    
}

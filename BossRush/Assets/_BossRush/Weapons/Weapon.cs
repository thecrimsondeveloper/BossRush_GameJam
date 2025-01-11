using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    Player currentAttacker;
    public AudioClip attackSound;

    public virtual void Use(Player attacker)
    {
        currentAttacker = attacker;
        WhenUsed(attacker);
    }

    protected abstract void WhenUsed(Player attacker);


    public void Attack()
    {
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
        TryAttack();
    }

    protected abstract void TryAttack();

}

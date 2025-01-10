using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    Player currentAttacker;

    public virtual void Use(Player attacker)
    {
        currentAttacker = attacker;
        WhenUsed(attacker);
    }

    protected abstract void WhenUsed(Player attacker);


    public void Attack()
    {
        TryAttack();
    }

    protected abstract void TryAttack();

}

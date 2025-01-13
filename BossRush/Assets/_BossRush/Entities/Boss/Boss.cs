using UnityEngine;

public class Boss : MonoBehaviour
{   
    public float health;
    public BossPhaseDefinition[] bossPhases;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        

        RefreshPhases();


    }

    private void RefreshPhases()
    {
        foreach (var phase in bossPhases)
        {
            if (health <= phase.healthActivationThreshold)
            {
                phase.phaseObject.Activate();
            }
            else if (health >= phase.healthDeactivationThreshold)
            {
                phase.phaseObject.Deactivate();
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}


[System.Serializable]
public class BossPhaseDefinition
{
    public float healthActivationThreshold;
    public float healthDeactivationThreshold;

    public BossPhase phaseObject;
}

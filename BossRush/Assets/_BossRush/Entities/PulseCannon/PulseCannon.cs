using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.VFX;
using UnityEngine.PlayerLoop;
using System.Transactions;

public class PulseCannon : MonoBehaviour
{
    [SerializeField] Transform anchorToAim;
    [SerializeField] Rigidbody pulseCannonRepulsor;
    [SerializeField] Transform bulletSpawnPoint;
    private Player trackedPlayer = null;

    public float chargeTime = 1f;
    public float shootForce = 1000f;
    public float trackingDistance = 10f;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            trackedPlayer = player;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            trackedPlayer = null;
        }
    }


    float timeUntilShoot = 1;
    void Update()
    {
        if(trackedPlayer !=null)
        {
            timeUntilShoot -= Time.deltaTime;
            if(timeUntilShoot <= 0)
            {
                Shoot();
                timeUntilShoot = chargeTime;
            }

            anchorToAim.LookAt(trackedPlayer.transform);


            if(Vector3.Distance(trackedPlayer.transform.position, transform.position) > trackingDistance)
            {
                trackedPlayer = null;
            }
        }

    }

    void Shoot()
    {
        Rigidbody pulse = Instantiate(pulseCannonRepulsor, bulletSpawnPoint.position, Quaternion.identity);
        pulse.AddForce(anchorToAim.forward * shootForce);


    }



}

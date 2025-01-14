using UnityEngine;

public interface ILevelInteractor
{
    public void OnTriggerEnter(Collider other);
    public void OnTriggerExit(Collider other);
}

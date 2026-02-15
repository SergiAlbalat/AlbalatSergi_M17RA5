using UnityEngine;

public class Belt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.EquipItem();
            }
        }
    }
}

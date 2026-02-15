using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatingItem beltItem;
    [SerializeField] private GameObject beltEquipment;
    private void Awake()
    {
        beltItem.gameObject.SetActive(true);
        beltEquipment.SetActive(false);
    }
    public void EquipItem()
    {
        beltEquipment.SetActive(true);
        beltItem.gameObject.SetActive(false);
    }
}

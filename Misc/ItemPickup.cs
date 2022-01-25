using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]private int score = 10;
    public enum ItemType {normal,invincibility};
    [SerializeField] private ItemType itemType = ItemType.normal;

    public int getScore()=>score;

    public ItemType getItemType()=>itemType;
}

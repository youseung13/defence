using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    public Text itemCountText;
    public Item currentItem;

    public void SetSlot(Item newItem)
    {
        currentItem = newItem;
        itemImage.sprite = newItem.itemData.itemSprite;
        itemCountText.text = newItem.itemCount.ToString();
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemImage.sprite = null;
        itemCountText.text = "";
    }
}
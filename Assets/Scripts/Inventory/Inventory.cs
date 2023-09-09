using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;
    public Item[] items;

    public void AddtoInventory(Item newitem, int Count)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].currentItem != null)
            {
                if(slots[i].currentItem == newitem)
                {
                    slots[i].currentItem.itemCount += Count;
                    slots[i].itemCountText.text = slots[i].currentItem.itemCount.ToString();
                    return;
                }
            }
            else
            {
                slots[i].currentItem = newitem;
                slots[i].itemImage.sprite = newitem.itemData.itemSprite;
                slots[i].itemCountText.text = newitem.itemCount.ToString();
                return;
            }

            
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddtoInventory(items[0], items[0].itemCount);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            AddtoInventory(items[1], items[1].itemCount);
        }
         if(Input.GetKeyDown(KeyCode.N))
        {
            AddtoInventory(items[2], items[2].itemCount);
        }
    }

    public void UpdateSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            
        }
    }
    // itemdata scriptableobject  write script please
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Item_Management;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform =  Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDirection = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDirection * 3f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDirection * 3f, ForceMode2D.Impulse);
        return itemWorld;
    }
    
    private Item item;
    private SpriteRenderer spriteRenderer;
    
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}

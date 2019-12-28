using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject platform;

    public GameObject skin; 
    
    public BlockType blockType; 

    private void Start()
    {
        platform = transform.parent.gameObject;
    }

    public void SetSkin(string theme)
    {
        skin = Instantiate(Resources.Load<GameObject>("Textures/BlockSkins/" + theme + "/" + BlockTypeExtension.ToFriendlyString(blockType)), Vector3.zero, Quaternion.identity);
        skin.transform.SetParent(transform, false);
    }
}

public enum BlockType
{
    Empty,
    Plain1, 
    Plain2,
    Plain3, 
    Plain4, 
    Plain5, 
    Invisible, 
    Car, 
    River, 
    MovingBlocks
}


public static class BlockTypeExtension
{
    public static string ToFriendlyString(this BlockType me)
    {
        switch(me)
        {
            case BlockType.Empty:
                return "Empty";
            case BlockType.Plain1:
                return "Plain1";
            case BlockType.Plain2:
                return "Plain2";
            case BlockType.Plain3:
                return "Plain3";
            case BlockType.Plain4:
                return "Plain4";
            case BlockType.Plain5:
                return "Plain5";
            case  BlockType.Invisible:
                return "Invisible";
            case BlockType.River:
                return "River";
            case BlockType.Car:
                return "Car";
            case BlockType.MovingBlocks:
                return "MovingBlocks";
            default:
                return "Get your damn dirty hands off me you FILTHY APE!";
        }
    }
}

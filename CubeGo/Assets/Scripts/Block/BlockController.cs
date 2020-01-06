using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockController : MonoBehaviour
{
    public GameObject skin; 
    
    public BlockType blockType;

    public Vector3 speed = Vector3.zero;

    public bool isTimber;

    public void SetSkin(string theme) // skin needs to instantiated from pool or something like this 
    {
        skin = Instantiate(Resources.Load<GameObject>("Textures/" + theme + "/BlockSkins/" + BlockTypeExtension.ToFriendlyString(blockType)), Vector3.zero, Quaternion.identity);
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
    River,
    MovingBlocks,
    RoadLight,
    RoadDark, // is darker than road1 
    Arrow, 
    Spikes, 
    Axe, 
    Saw,
    SawEnd,
    SawStart,
    FallingBlocks,
    Invisible, 
    Edge,
    Roll, 
    None
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
            case  BlockType.River:
                return "River";
            case BlockType.MovingBlocks:
                return "MovingBlocks";
            case BlockType.RoadLight:
                return "RoadLight";
            case BlockType.RoadDark:
                return "RoadDark";
            case  BlockType.Arrow:
                return "Arrow";
            case BlockType.Spikes:
                return "Spikes";
            case BlockType.Axe:
                return "Axe";
            case  BlockType.Saw:
                return "Saw";
            case BlockType.SawStart:
                return "SawStart";
            case BlockType.SawEnd:
                return "SawEnd";
            case BlockType.Invisible:
                return "Invisible";
            case  BlockType.FallingBlocks:
                return "FallingBlocks";
            case BlockType.Edge:
                return "Edge"; 
            case BlockType.None:
                return "None";
            case BlockType.Roll:
                return "Roll";
            default:
                return "Fuck this game"; 
        }
    }
}

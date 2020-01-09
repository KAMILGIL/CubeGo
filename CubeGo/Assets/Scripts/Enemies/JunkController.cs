using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkController : MonoBehaviour
{
    private GameObject junkSkin;

    public void SetJunk(Vector3 begin, Vector3 end, Vector3 speed)
    {
        junkSkin = Resources.Load<GameObject>("Textures/EnemySkin/Junk");
    }
}

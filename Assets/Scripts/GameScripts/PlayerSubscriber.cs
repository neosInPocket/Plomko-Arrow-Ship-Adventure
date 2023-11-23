using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public abstract class PlayerSubscriber : MonoBehaviour
{
    public abstract void OnPlayerGold(int allGold);
    public abstract void OnPlayerDamaged(int lifeCount);
}

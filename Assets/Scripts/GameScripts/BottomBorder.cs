using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Barrier>(out Barrier barrier))
        {
            if (barrier.IsInitial) return;
            Destroy(barrier.gameObject);
        }
        
        if (other.TryGetComponent<GoldCoin>(out GoldCoin coin))
        {
            if (coin.Collected) return;
            Destroy(coin.gameObject);
        }
    }
}

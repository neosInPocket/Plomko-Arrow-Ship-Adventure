using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class PlayerMagnetTrigger : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float attractSpeed;
    private List<GoldCoin> attractedCoins;
    private List<GoldCoin> coinsToDelete;
    
    private void Start()
    {
        attractedCoins = new List<GoldCoin>();
        coinsToDelete = new List<GoldCoin>();
    }

    private void Update()
    {
        transform.position = target.position;

        foreach (var coin in attractedCoins)
        {
            if (coin == null)
            {
                coinsToDelete.Add(coin);
                continue;
            }
            
            if (!coin.IsActive) continue;

            var destination = transform.position - coin.transform.position;
            var distance = destination.magnitude;

            coin.Rigid.velocity = attractSpeed * destination.normalized / (distance + 0.1f);
        }

        foreach (var coin in coinsToDelete)
        {
            attractedCoins.Remove(coin);
        }
        
        coinsToDelete.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GoldCoin>(out GoldCoin coin))
        {
            attractedCoins.Add(coin);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<GoldCoin>(out GoldCoin coin))
        {
            if (attractedCoins.Contains(coin))
            {
                attractedCoins.Remove(coin);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerSavesLoad playerSavesLoad;
    [SerializeField] private MainGameMachine mainGameMachine;
    [SerializeField] private GameObject deathVFX;
    private int currentLifes;
    private int currentPoints;

    public void Restart()
    {
        currentLifes = playerSavesLoad.Data.playerMaxLifes;
        currentPoints = 0;
        
        spriteRenderer.enabled = true;
        trail.enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GoldCoin>(out GoldCoin goldCoin))
        {
            if (!goldCoin.IsActive) return;
            
            currentPoints += 2;
            mainGameMachine.OnPlayerGold(currentPoints);
            goldCoin.Explode();
        }

        if (other.TryGetComponent<Barrier>(out Barrier barrier))
        {
            if (!barrier.IsActive) return;
            
            currentLifes--;
            if (currentLifes <= 0)
            {
                currentLifes = 0;
                StartCoroutine(DeathHandler());
            }
            else
            {
                StartCoroutine(DamageHandler());
            }
            
            mainGameMachine.OnPlayerDamaged(currentLifes);
        }
    }

    private IEnumerator DamageHandler()
    {
        Color color = spriteRenderer.color;
        
        for (int k = 0; k < 7; k++)
        {
            color.a = 0.3f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.16743f);
            
            color.a = 1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.16743f);
        }
    }
    
    private IEnumerator DeathHandler()
    {
        spriteRenderer.enabled = false;
        trail.enabled = false;
        var deathFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(deathFX.gameObject);
    }
}

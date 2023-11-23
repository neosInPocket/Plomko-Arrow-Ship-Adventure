using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GoldCoin : Spawnable
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject collectExplosion;
    public bool Collected { get; set; }
    public Rigidbody2D Rigid => rigid;
    
    public void Explode()
    {
        if (Collected) return;
        Collected = true;
        StartCoroutine(ExplodeEffect());
    }
    
    private IEnumerator ExplodeEffect()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        particles.Stop();
        spriteRenderer.enabled = false;
        var explode = Instantiate(collectExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    protected override void CustomInitialize()
    {
        if (!IsActive)
        {
            particles.gameObject.SetActive(false);
        }
    }

    protected override void CustomActiveState(bool state)
    {
        if (state)
        {
            particles.gameObject.SetActive(true);
        }
        else
        {
            particles.gameObject.SetActive(false);
        }
    }
}

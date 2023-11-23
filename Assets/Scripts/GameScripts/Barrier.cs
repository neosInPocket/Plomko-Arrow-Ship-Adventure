using UnityEngine;
using Random = UnityEngine.Random;

public class Barrier : Spawnable
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private ParticleSystem particles;
    private int rotationDirection;
    
    private void FixedUpdate()
    {
        var angles = transform.rotation.eulerAngles;
        angles.z += rotationDirection * rotationSpeed;
        transform.rotation = Quaternion.Euler(angles);
    }

    protected override void CustomInitialize()
    {
        var rnd = Random.Range(0, 2);
        if (rnd == 1)
        {
            rotationDirection = 1;
        }
        else
        {
            rotationDirection = -1;
        }
    }

    protected override void CustomActiveState(bool state)
    {
        particles.gameObject.SetActive(state);
    }
}

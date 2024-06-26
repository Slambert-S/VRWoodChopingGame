using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportationParticleManager : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem refParticle;
    void OnEnable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.onTeleportationParticleChange += TriggerParticle;
        }
    }
    void OnDisable()
    {
        if (GameEvents.current == null)
        {
            Debug.LogWarning("GameEvent is not yet created");
        }
        else
        {
            GameEvents.onTeleportationParticleChange -= TriggerParticle;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TriggerParticle(bool value)
    {
        if (value)
        {
            refParticle.Play();
        }
        else
        {
            refParticle.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFX_Oneshot : MonoBehaviour
{
    public List<CustomParticleList> particleList;
    [Header("Can leave blank to auto calculate death time")]
    public bool autoCalculateDeathTime = true;
    public float deathTime;
    private void Awake()
    {
        if (autoCalculateDeathTime)
        {
            //Auto calculate deathtime
            foreach (CustomParticleList customParticle in particleList)
            {
                deathTime += customParticle.waitTimer + customParticle.particleFx.main.duration;
            }
        }
    }
    private void Start()
    {
        StartCoroutine(playParticleFx());
    }

    IEnumerator playParticleFx()
    {
        foreach (CustomParticleList customParticle in particleList)
        {
            yield return new WaitForSeconds(customParticle.waitTimer);
            customParticle.particleFx.Play();
        }
        Destroy(gameObject, deathTime);
    }
}

[System.Serializable]
public class CustomParticleList
{
    public string name;
    public ParticleSystem particleFx;
    public float waitTimer;
}
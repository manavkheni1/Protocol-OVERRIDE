using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("RAM Specs")]
    public float maxRAM = 100f; 
    public float currentRAM;
    public float regenRate = 8f; 
    public float regenDelay = 1.5f; 

    private float lastConsumeTime = -10f; 
    public bool isRegenPaused = false; 

    void Start()
    {
        currentRAM = maxRAM; 
    }

    void Update()
    {
        if (!isRegenPaused && currentRAM < maxRAM && Time.time >= lastConsumeTime + regenDelay)
        {
            currentRAM += regenRate * Time.deltaTime;

            if (currentRAM > maxRAM)
            {
                currentRAM = maxRAM;
            }
        }
    }

    public bool TryConsumeRAM(float amount)
    {
        if (currentRAM >= amount)
        {
            currentRAM -= amount;
            lastConsumeTime = Time.time; 
            return true; 
        }
        
        return false; 
    }
}
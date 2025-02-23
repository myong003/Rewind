using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Slider bossHealthSlider;
    public EntityCombat boss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHealthSlider.maxValue = boss.maxHealth;
        bossHealthSlider.value = boss.getCurrentHealth();    
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthSlider.value = boss.getCurrentHealth();    
    }
}

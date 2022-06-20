using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarScript : MonoBehaviour
{
    public TextMeshProUGUI bossName;
    public Slider slider;

    public void SetBossName(string name)
    {
        bossName.text = name;
    }

    public void SetUIHealthBarToActive()
    {
        slider.gameObject.SetActive(true);
    }

    public void SetBossMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetBossCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
}

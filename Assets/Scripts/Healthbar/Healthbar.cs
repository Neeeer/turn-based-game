using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    public UnityEngine.UI.Slider healthSlider;
    public UnityEngine.UI.Image healthSlide;
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image healthUI;
    public Text healthNumber;
    List<UnityEngine.UI.Image> HealthListUI;

    // Start is called before the first frame update
    void Awake()
    {
        HealthListUI = new List<UnityEngine.UI.Image>();

        HealthListUI.Add(healthSlide);
        HealthListUI.Add(healthBar);
        HealthListUI.Add(healthUI);

        removeHealthBar();
    }

    public void removeHealthBar()
    {
        foreach (UnityEngine.UI.Image i in HealthListUI)
        {
            i.enabled = (false);
        }
        healthNumber.enabled = false;
    }

    public void updateHealthBar(Character cha)
    {
        foreach (UnityEngine.UI.Image i in HealthListUI)
        {
            i.enabled = true;
        }

        healthSlider.value = (float)cha.Health / cha.MaxHeath;
        healthNumber.enabled = true;
        healthNumber.text = cha.Health.ToString() + "/" + cha.MaxHeath.ToString();
    }

   
}

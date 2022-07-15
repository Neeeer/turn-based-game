using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    private TextMeshPro text;
    private float limit;
    private Color textC;


    // create a damage popUp
    public static void Create(Transform t, Vector3 pos, int dmg)
    {
        Transform damageTrans = Instantiate(t, pos, Quaternion.identity);

        DamagePopup damage = damageTrans.GetComponent<DamagePopup>();
        damage.Setup(dmg);
    }

   

    public void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }

    // call set uo after creating the damage popUp in order set text color depending on what the ability does or if it heals
    public void Setup(int damage)
    {
        textC = text.color;
        limit = 1f;

        if (damage <0)
        {
            Debug.Log("heals");
            textC.g = 1;
            textC.r = 0;
            text.color = textC;
            damage = damage * -1;
        }
        text.SetText(damage.ToString());
    }

    // Update is called once per frame
    // move the damage popUp upwards as the text color alpha is reduces, upon the alpha becoming 0 desteroy the gameobject.
    public void Update()
    {
        float speedy = 0.3f;
        transform.position += new Vector3(0, speedy) * Time.deltaTime;

        limit -= Time.deltaTime;

        if (limit < 0)
        {
            float disappearSpeed = 1f;
            textC.a -= disappearSpeed * Time.deltaTime;
            text.color = textC;

            if (textC.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    private TextMeshPro text;
    private float limit;
    private Color textC;

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

    // Start is called before the first frame update
    public void Setup(int damage)
    {
        textC = text.color;
        limit = 1f;

        if (damage <0)
        {
            textC.g = 1;
            textC.r = 0;
            text.color = textC;
            damage = damage * -1;
        }
        text.SetText(damage.ToString());
    }

    // Update is called once per frame
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


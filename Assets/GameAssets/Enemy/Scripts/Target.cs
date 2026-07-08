using TMPro;
using UnityEngine;

public class Target : Enemy
{
    public TextMeshPro text;


    private void Awake()
    {
        if (text != null)
        {
            text.text = "0";
        }
    }

    public override void TakeDamage(float damage)
    {
        text.text = damage.ToString();
    }
}

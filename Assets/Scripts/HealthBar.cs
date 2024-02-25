using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Script bar de vie 
public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    private Image barImage;
    [SerializeField]
    private Transform player;
    
    void Start()
    {
        bar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();
        //Bar de vie deviens rouge a 1/3
        if (Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        SetSize(Health.totalHealth);
    }

    public void Damage(float damage)
    {
        if((Health.totalHealth -= damage) >= 0f)
        {
            Health.totalHealth -= damage;
        }
        else
        {
            Health.totalHealth = 0f;
            Destroy(
                player.gameObject);
        
        }

        if(Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }

        SetSize(Health.totalHealth);
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}
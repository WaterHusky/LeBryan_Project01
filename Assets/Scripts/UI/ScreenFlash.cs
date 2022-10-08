using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image panel;
    [SerializeField] private float totalFlashTime;

    private void OnEnable()
    {
        playerHealth.TookDamage += StartFlash;
    }

    private void OnDisable()
    {
        playerHealth.TookDamage -= StartFlash;
    }

    private void StartFlash(int damage)
    {
        StartCoroutine(Flash(damage));
    }

    float flashAlpha = 0;

    private IEnumerator Flash(int damage)
    {
        //fade flash in
        flashAlpha = 1;
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);

        yield return 0;

        //fade flash out
        Color tempColor = panel.color; 
        while (panel.color.a > 0.1f)
        {
            flashAlpha = Mathf.Lerp(flashAlpha,0,Time.deltaTime);
            tempColor.a = flashAlpha;
            panel.color = tempColor;
            yield return 0;
        }

        flashAlpha = 0;
        tempColor.a = 0;
        panel.color = tempColor;
    }
}

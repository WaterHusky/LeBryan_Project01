using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private void OnEnable()
    {
        playerHealth.TookDamage += StartShake;
    }

    private void OnDisable()
    {
        playerHealth.TookDamage -= StartShake;
    }

    private void StartShake(int damage)
    {
        StartCoroutine(Shake(.15f,.4f,damage));
    }

    public IEnumerator Shake(float duration, float magnitude, int damage)
    {
        //save original position 
        Vector3 orignalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude * damage;
            float y = Random.Range(-1f, 1f) * magnitude * damage;

            transform.localPosition = new Vector3(x, y, orignalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = orignalPos;

    }
} 


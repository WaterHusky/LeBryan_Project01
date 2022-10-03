using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIndicator : MonoBehaviour
{
    [SerializeField] private BoxCollider bossCollider;
    private bool visible;

    private void Awake()
    {
        HideIndicator();
    }

    private void OnEnable()
    {
        bossCollider.gameObject.GetComponent<BossMovement>().StartedChargeAttack += ShowIndicator;
        bossCollider.gameObject.GetComponent<BossMovement>().EndedChargeAttack += HideIndicator;
    }

    private void OnDisable()
    {
        bossCollider.gameObject.GetComponent<BossMovement>().StartedChargeAttack -= ShowIndicator;
        bossCollider.gameObject.GetComponent<BossMovement>().EndedChargeAttack -= HideIndicator;
    }

    private void Update()
    {
        if (visible)
        {
            float scale = 2 * bossCollider.size.z;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void ShowIndicator()
    {
        visible = true;
    }

    private void HideIndicator()
    {
        visible = false;
        transform.localScale = Vector3.zero;
    }
}

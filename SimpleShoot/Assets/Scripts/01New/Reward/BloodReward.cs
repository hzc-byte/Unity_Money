using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodReward : RewardBase
{
    public float addBlood = 20;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CompatController>().GetHealth(addBlood);
        }
    }
}

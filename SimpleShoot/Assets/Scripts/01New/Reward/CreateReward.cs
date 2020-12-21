using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    None,
    addBlood,
    addAttack
}

public class CreateReward : MonoBehaviour
{
    public void Create(RewardType type)
    {
        switch (type)
        {
            case RewardType.addBlood:
                GameObject addBlood = Instantiate(Resources.Load<GameObject>("Rewards/BloodReward"));
                addBlood.transform.SetParent(transform);
                addBlood.transform.localPosition = new Vector3(1.95f, 0, 0);
                addBlood.transform.localEulerAngles = Vector3.zero;
                addBlood.transform.localScale = new Vector3(5, 5, 5);
                addBlood.transform.SetParent(GameObject.Find("Reward").transform);
                break;
            case RewardType.addAttack:
                GameObject addAttack = Instantiate(Resources.Load<GameObject>("Rewards/AttackReward"));
                addAttack.transform.SetParent(transform);
                addAttack.transform.localPosition = new Vector3(1.95f, 0, 0);
                addAttack.transform.localEulerAngles = Vector3.zero;
                addAttack.transform.localScale = new Vector3(5, 5, 5);
                addAttack.transform.SetParent(GameObject.Find("Reward").transform);
                break;
        }
    }
}

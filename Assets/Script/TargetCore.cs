using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCore : MonoBehaviour {

    public EffectCore effect;
    float lifeTime = 5f;
    Rigidbody rigid;
    float moveSpeed = 5;
    void Start()
    {
        StartCoroutine(LifeTime());
        rigid = GetComponent<Rigidbody>();

        var x = Random.value*2 - 1; // -1 .. 1 
        rigid.velocity = new Vector3(x, 1, 0)* moveSpeed;


    }

    public bool RecieveEffect(EffectCore core)
    {
        Debug.Log("self " + effect.m_EffectType.ToString() + " income " + core.m_EffectType.ToString());
        if (effect.m_EffectType == core.m_EffectType)
        {

            switch (effect.m_EffectType)
            {
                case EffectType.White:
                    SceneController.instance.WhiteCount += 1;
                    break;
                case EffectType.Red:
                    SceneController.instance.RedCount += 1;
                    break;
                case EffectType.Green:
                    SceneController.instance.GreenCount += 1;
                    break;
                case EffectType.Black:
                    SceneController.instance.BlackCount += 1;
                    break; 
            }

            Destroy(gameObject);

        }


        return effect.m_EffectType != core.m_EffectType;
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}

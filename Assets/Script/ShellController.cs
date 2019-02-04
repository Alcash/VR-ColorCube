using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour {

    public EffectCore effectcore;
    float lifeTime = 5f;
    Material material;

	// Use this for initialization
	void Start () {
        StartCoroutine(LifeTime());

        material = GetComponent<Renderer>().sharedMaterial;

        material.color = effectcore.GetColor();

    }	

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " + other.name + " tag "+ other.tag);
        if(other.tag == "Target")
        {
            other.GetComponent<TargetCore>().RecieveEffect(effectcore);
        }
    }

    
}

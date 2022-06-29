using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHand : MonoBehaviour {

   
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {



    }

    SpawnManager getSpawnManager()
    {
        GameObject smobj = GameObject.Find("BoxSpawner") as GameObject;
        SpawnManager sm = smobj.GetComponent<SpawnManager>();
        return sm;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.MissCollider.ToString())
        {
            
            getSpawnManager().SoundPlay(SoundName.missBox);

            //Debug.Log("잘못때림");
            other.gameObject.transform.parent.gameObject.GetComponent<TargetBox>().isMove = false;
            other.gameObject.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().useGravity = true;

            StartCoroutine(MissEffect(other.transform));
      
        }
    }

    IEnumerator MissEffect(Transform boxTr)
    {
        yield return new WaitForSeconds(0.6f);
        
        if (boxTr != null) { 
            getSpawnManager().EffectInst(EffectName.missBoxEffect, boxTr.position);
        }
        yield return new WaitForSeconds(0.2f);
        if (boxTr != null) Destroy(boxTr.gameObject.transform.parent.gameObject, 0.5f);
    } 
    
    
}

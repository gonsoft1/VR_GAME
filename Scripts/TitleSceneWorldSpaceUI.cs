using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneWorldSpaceUI : MonoBehaviour {

    public Transform _target;    
    public Camera vrCamera;
    Transform _worldUICanvasTr;

    // Use this for initialization
    void Start () {
        
        _worldUICanvasTr = GameObject.Find("Canvas").transform;
        transform.SetParent(_worldUICanvasTr);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    public void InitUI(Transform target)
    {
        _worldUICanvasTr = GameObject.Find("Canvas").transform;
        transform.SetParent(_worldUICanvasTr);

        _target = target;
    }*/

    void LateUpdate()
    {
       //transform.rotation = Quaternion.LookRotation(vrCamera.transform.forward);
       transform.position = _target.position;
    }
}

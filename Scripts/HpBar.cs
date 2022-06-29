using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    public Image _hpBarProgress;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SettingHpBar(int hp)
    {
        float fHp = hp * 0.01f;
        _hpBarProgress.fillAmount = fHp;
    }

}

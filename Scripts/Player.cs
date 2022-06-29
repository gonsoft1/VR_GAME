using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WeaponType { normalSword };

public class Player : MonoBehaviour {

    public int playerHp;
    private int normalSword_Damage = 100;
    public int normalSword_SkillDamage;   
    public WeaponType currentWeapon;
    public GameObject[] lifeCandles;

    public void SetCurrentWeapon(WeaponType wt)
    {
        currentWeapon = wt;
    }

    public WeaponType GetCurrentWeapon()
    {
        return currentWeapon;
    }


    // Use this for initialization
    void Start () {
        getDragon().InitDragon();
        this.currentWeapon = WeaponType.normalSword;
	}
	
	// Update is called once per frame
	void Update () {


     
	}



    Dragon getDragon()
    {
        GameObject dragonObj = GameObject.Find("Dragon") as GameObject;
        Dragon dragonScript = dragonObj.GetComponent<Dragon>();
        return dragonScript;
    }
    
    public int CalcAttackDamage()
    {
        if (currentWeapon == WeaponType.normalSword)
        {
            return normalSword_Damage;
        }
        
        return 0;
        
    }

    public void DecreasePlayerHp()
    {
        if(playerHp > 0 ) lifeCandles[playerHp - 1].SetActive(false);

        --playerHp;
        
        if(playerHp <= 0)
        {
            PlayerDie();
        }
    }

    public void PlayerDie()
    {

        // 게임오버 패배 씬 전환
        SceneManager.LoadScene("LoseScene");
        
    }

}

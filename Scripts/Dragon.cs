using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dragon : MonoBehaviour {

    public Transform[] effectCreatePos;
    public int dragonHp;
    public float dragonAttackDamage;
    public Animator dragonAnimator;

	// Use this for initialization
	void Start () {
        dragonAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
      
    }

    public void InitDragon()
    {
        dragonHp = 100;
    }

    public int GetDragonHp()
    {
        return dragonHp;
    }

    SpawnManager getSpawnManager()
    {
        GameObject smobj = GameObject.Find("BoxSpawner") as GameObject;
        SpawnManager sm = smobj.GetComponent<SpawnManager>();
        return sm;
    }

    public void DecreaseDragonHp(int playerDamage)
    {
        if (dragonHp > 0)
        {
            dragonHp -= playerDamage;
        }

        if (dragonHp <= 0)
        {
            getSpawnManager().isGameEnd = true;

            GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("TargetBox");
            GameObject[] enemyAttackBoxes = GameObject.FindGameObjectsWithTag("EnemyAttackBox");
            for(int i = 0; i < allBoxes.Length; i++)
            {
                getSpawnManager().EffectInst(EffectName.missBoxEffect, allBoxes[i].transform.position);
                Destroy(allBoxes[i]);
            }
            for(int i = 0; i < enemyAttackBoxes.Length; i ++)
            {
                getSpawnManager().EffectInst(EffectName.missBoxEffect, enemyAttackBoxes[i].transform.position);
                Destroy(enemyAttackBoxes[i]);
            }

            GameObject.Find("Canvas").transform.Find("HpBar").gameObject.SetActive(false);            
            Invoke("DragonFall", 0.5f);
        }
                
        HpBar _hpbar = GameObject.Find("Canvas").transform.Find("HpBar").GetComponent<HpBar>();
        _hpbar.SettingHpBar(dragonHp);

        Debug.Log(dragonHp);

    }
    
    void DragonFall()
    {
        dragonAnimator.SetBool("IsDragonFall", true);
        Invoke("DragonDie", 1f);
    }

    void DragonDie()
    {
        dragonAnimator.SetBool("IsDragonDie", true);
        getSpawnManager().SoundPlay(SoundName.dragonDeadSound);

        Invoke("SceneChange", 5.5f);
    }    

    void SceneChange()
    {
        SceneManager.LoadScene("WinScene");
    }
}

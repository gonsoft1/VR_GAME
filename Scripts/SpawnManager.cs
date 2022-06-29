using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum emBoxNum
{
    First = 0,
    Middle,
    Last,
    Bomb
};

public enum BoxKind { TargetBox, EnemyAttackBox };
public enum Tag { Player, TargetBox, MissCollider, Sword, Shield };
public enum DirectionName { Left, Right, Center };
public enum SoundName { hitBox, missBox , blockBox, dragonAttackSound, dragonStartSound, dragonDeadSound, winSound, loseSound };
public enum EffectName { hitBoxEffect, missBoxEffect, Skill_1 , Skill_2 , Skill_3 , Skill_4 , Skill_5 , Skill_6 , createBoxEffect };

public class SpawnManager : MonoBehaviour {

    List<bool> checkComboList = new List<bool>();

    // 사운드
    public AudioSource effectSoundSpeaker;
    public AudioSource dragonSoundSpeaker;
    public AudioSource bgmSpeaker;
    public AudioClip hitBoxSound;
    public AudioClip missBoxSound;
    public AudioClip blockBoxSound;

    public AudioClip dragonAttackSound;
    public AudioClip dragonStartSound;
    public AudioClip dragonDeadSound;

    public AudioClip winSound;
    public AudioClip loseSound;

    // 기술, 타격 이펙트
    public Object hitEffectPrefab;
    public Object missEffectPrefab;
    public Object skill_1_EffectPrefab;
    public Object skill_2_EffectPrefab;
    public Object skill_3_EffectPrefab;
    public Object skill_4_EffectPrefab;
    public Object skill_5_EffectPrefab;
    public Object skill_6_EffectPrefab;
    public Object createBoxEffectPrefab;

    public GameObject[] targetBoxPrefab;    // 0 위쪽 , 1 오른쪽 , 2 아래쪽 , 3 왼쪽 , 4 적의 공격
          
    public Transform[] BoxCreatePos;    // 0 왼쪽    ,   1 오른쪽
    public Transform EnemyAttackBoxCreatePos;
    public Transform playerPosLeft;
    public Transform playerPosRight;
    public Transform effectCreatePos;
    public Transform[] dragonEffectPos;

    public Transform effectCreatePos1;
    public Transform effectCreatePos2;

    public Transform playerPosLeftRND;
    public Transform playerPosRightRND;

    public int currentboxPatternNum;
    public int whatCreatePosLeftOrRight;    // 0 왼쪽 , 1 오른쪽 , 2 센터

    public float ranNumMax;
    public float ranNumMin;

    public bool isOpening = true;
    public bool isGameEnd = false;

    public BoxKind currentCreateBoxKind;   
    
    // Use this for initialization
    IEnumerator Start () {
        while (!isGameEnd)
        {
            if(isOpening)
            {
                yield return new WaitForSeconds(0.3f);                
                SoundPlay(SoundName.dragonStartSound);
                yield return new WaitForSeconds(0.5f);
                getDragon().dragonAnimator.SetTrigger("Screaming");
                yield return new WaitForSeconds(5f);
                getDragon().dragonAnimator.SetTrigger("ScreamEnd");
                yield return new WaitForSeconds(2f);
                getDragon().dragonAnimator.SetBool("IsDragonFlying", true);
                bgmSpeaker.Play();
                isOpening = false;

                yield return new WaitForSeconds(2.5f);
                
                GameObject _hpbar = GameObject.Find("Canvas").transform.Find("HpBar").gameObject as GameObject;
                _hpbar.SetActive(true);
            }

            currentboxPatternNum = Random.Range(1, 7);
           
            switch (currentboxPatternNum)
            {
                case 1:
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.First);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    RightTargetBoxInst(emBoxNum.Middle);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.Last);
                    yield return new WaitForSeconds(1.8f);
                    EnemyAttackBoxInst(emBoxNum.Bomb);                    
                    break;
                case 2:
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    TopTargetBoxInst(emBoxNum.First);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    BottomTargetBoxInst(emBoxNum.Last);
                    yield return new WaitForSeconds(1.8f);
                    EnemyAttackBoxInst(emBoxNum.Bomb);                    
                    break;
                case 3:
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    TopTargetBoxInst(emBoxNum.First);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.Middle);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    BottomTargetBoxInst(emBoxNum.Last);
                    yield return new WaitForSeconds(1.8f);
                    EnemyAttackBoxInst(emBoxNum.Bomb);
                    break;
                case 4:
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    RightTargetBoxInst(emBoxNum.First);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    TopTargetBoxInst(emBoxNum.Middle);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.Last);
                    yield return new WaitForSeconds(1.8f);
                    EnemyAttackBoxInst(emBoxNum.Bomb);
                    break;
                case 5:
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    BottomTargetBoxInst(emBoxNum.First);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    RightTargetBoxInst(emBoxNum.Middle);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.Last);
                    yield return new WaitForSeconds(1.8f);
                    EnemyAttackBoxInst(emBoxNum.Bomb);
                    break;
                case 6:
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.First);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    RightTargetBoxInst(emBoxNum.Middle);
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    LeftTargetBoxInst(emBoxNum.Middle);                        
                    yield return new WaitForSeconds(Random.Range(ranNumMin, ranNumMax));
                    RightTargetBoxInst(emBoxNum.Last);
                    yield return new WaitForSeconds(1.8f);
                    EnemyAttackBoxInst(emBoxNum.Bomb);
                    break;  
            }            
        }        
    }
    	
	// Update is called once per frame
	void Update () {
        
	}

    void TopTargetBoxInst(emBoxNum boxNum)
    {
        if (isGameEnd) return;
        
        int ranNum = Random.Range(0,2);
        EffectInst(EffectName.createBoxEffect, BoxCreatePos[ranNum].position);
        GameObject topBox = Instantiate(targetBoxPrefab[0], BoxCreatePos[ranNum].position, Quaternion.identity) as GameObject;
        whatCreatePosLeftOrRight = ranNum;
        currentCreateBoxKind = BoxKind.TargetBox;
        topBox.GetComponent<TargetBox>().boxNum = boxNum;
    }

    void RightTargetBoxInst(emBoxNum boxNum)
    {
        if (isGameEnd) return;
        EffectInst(EffectName.createBoxEffect, BoxCreatePos[1].position);
        GameObject rightBox = Instantiate(targetBoxPrefab[1], BoxCreatePos[1].position, Quaternion.identity) as GameObject;
        whatCreatePosLeftOrRight = 1;
        currentCreateBoxKind = BoxKind.TargetBox;
        rightBox.GetComponent<TargetBox>().boxNum = boxNum;
        rightBox.GetComponent<TargetBox>().leftRightTargetBox = true;

        // y 축만 랜덤하게 해야하는데 제대로 변경 안됨
        // playerPosRightRND.position = new Vector3(0, Random.Range(0.25f, -0.25f), 0);
    }

    void BottomTargetBoxInst(emBoxNum boxNum)
    {
        if (isGameEnd) return;
        int ranNum = Random.Range(0, 2);
        EffectInst(EffectName.createBoxEffect, BoxCreatePos[ranNum].position);
        GameObject bottomBox = Instantiate(targetBoxPrefab[2], BoxCreatePos[ranNum].position, Quaternion.identity) as GameObject;
        whatCreatePosLeftOrRight = ranNum;
        currentCreateBoxKind = BoxKind.TargetBox;
        bottomBox.GetComponent<TargetBox>().boxNum = boxNum;
    }

    void LeftTargetBoxInst(emBoxNum boxNum)
    {
        if (isGameEnd) return;
        EffectInst(EffectName.createBoxEffect, BoxCreatePos[0].position);
        GameObject leftBox = Instantiate(targetBoxPrefab[3], BoxCreatePos[0].position, Quaternion.identity) as GameObject;
        whatCreatePosLeftOrRight = 0;
        currentCreateBoxKind = BoxKind.TargetBox;
        leftBox.GetComponent<TargetBox>().boxNum = boxNum;
        leftBox.GetComponent<TargetBox>().leftRightTargetBox = true;

        // y축만 랜덤하게 해야하는데 제대로 변경 안됨
       // playerPosLeftRND.position = new Vector3(, Random.Range(0.25f, -0.25f), 0);
    }

    void EnemyAttackBoxInst(emBoxNum boxNum)
    {
        if (isGameEnd) return;
        GameObject EnemyBox = Instantiate(targetBoxPrefab[4], EnemyAttackBoxCreatePos.position, Quaternion.identity) as GameObject;
        whatCreatePosLeftOrRight = 2;
        currentCreateBoxKind = BoxKind.EnemyAttackBox;
        EnemyBox.GetComponent<TargetBox>().boxNum = boxNum;
    }

    public void StartCombo()
    {
        checkComboList.Clear();
    }

    public void InsertCheckCombo(bool hit)
    {
        checkComboList.Add(hit);
    }

    public void CompareCheckEndCombo(int patternNum)
    {
        bool effectPlay = false;

        if( (patternNum == 1 && checkComboList.Count == 3) ||
            (patternNum == 2 && checkComboList.Count == 2) ||
            (patternNum == 3 && checkComboList.Count == 3) ||
            (patternNum == 4 && checkComboList.Count == 3) ||
            (patternNum == 5 && checkComboList.Count == 3) ||
            (patternNum == 6 && checkComboList.Count == 4) )
        {
            foreach (bool check in checkComboList)
            {
                if (check)
                {
                    effectPlay = true;
                }
                else
                {
                    effectPlay = false;
                    break;
                }
            }
        }
        
        if (effectPlay) EffectInst(patternNum);
    }

    public void EffectInst(int patternNum)
    {
        switch (patternNum)
        {
            case 1:
                Debug.Log("첫번째 스킬 발동!");
                EffectInst(EffectName.Skill_1, effectCreatePos.position);
                break;
            case 2:
                Debug.Log("두번째 스킬 발동!");
                EffectInst(EffectName.Skill_2, effectCreatePos.position);
                break;
            case 3:
                Debug.Log("세번째 스킬 발동!");
                EffectInst(EffectName.Skill_3, effectCreatePos.position);
                break;
            case 4:
                Debug.Log("네번째 스킬 발동!");
                EffectInst(EffectName.Skill_4, effectCreatePos.position);
                break;
            case 5:
                Debug.Log("다섯번째 스킬 발동!");
                EffectInst(EffectName.Skill_5, effectCreatePos.position);
                break;
            case 6:
                EffectInst(EffectName.Skill_6, effectCreatePos.position);
                break;
        }
    }

    public void SoundPlay(SoundName name)
    {
        if(name == SoundName.hitBox)
        { 
            effectSoundSpeaker.clip = hitBoxSound;
            effectSoundSpeaker.Play();
        }
        else if(name == SoundName.missBox)
        { 
            effectSoundSpeaker.clip = missBoxSound;
            effectSoundSpeaker.Play();
        }
        else if(name == SoundName.blockBox)
        {
            effectSoundSpeaker.clip = blockBoxSound;
            effectSoundSpeaker.Play();
        }
        else if (name == SoundName.dragonAttackSound)
        {
            dragonSoundSpeaker.clip = dragonAttackSound;
            dragonSoundSpeaker.Play();
        }
        else if (name == SoundName.dragonStartSound)
        {
            dragonSoundSpeaker.clip = dragonStartSound;
            dragonSoundSpeaker.Play();
        }
        else if (name == SoundName.dragonDeadSound)
        {
            dragonSoundSpeaker.clip = dragonDeadSound;
            dragonSoundSpeaker.Play();
        }
        else if (name == SoundName.winSound)
        {

        }
        else if (name == SoundName.loseSound)
        {

        }

        // 스킬 사운드도 넣었으면..
    }

    Dragon getDragon()
    {
        GameObject dragonObj = GameObject.Find("Dragon") as GameObject;
        Dragon dragonScript = dragonObj.GetComponent<Dragon>();
        return dragonScript;
    }

    Player GetPlayer()
    {
        GameObject playerObj = GameObject.Find("Player") as GameObject;
        Player playerScript = playerObj.GetComponent<Player>();
        return playerScript;
    }

    public void EffectInst(EffectName name ,Vector3 pos)
    {
        if (name == EffectName.hitBoxEffect)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, pos, Quaternion.identity) as GameObject;
            Destroy(hitEffect, 2f);
            
            GameObject dHitEffect = Instantiate(hitEffectPrefab, dragonEffectPos[Random.Range(0, 3)].position, Quaternion.identity) as GameObject;
            Destroy(dHitEffect, 2f);
        }
        else if (name == EffectName.missBoxEffect)
        {
            GameObject missEffect = Instantiate(missEffectPrefab, pos, Quaternion.identity) as GameObject;
            Destroy(missEffect, 2f);
        }
        else if(name == EffectName.Skill_1)
        {
            GameObject skill_1_Effect = Instantiate(skill_1_EffectPrefab, effectCreatePos.position, Quaternion.identity) as GameObject;
            Destroy(skill_1_Effect, 2f);

            // 스킬대미지 들어감
            getDragon().DecreaseDragonHp(GetPlayer().normalSword_SkillDamage);
        }
        else if (name == EffectName.Skill_2)
        {
            GameObject skill_2_Effect = Instantiate(skill_2_EffectPrefab, effectCreatePos.position, Quaternion.identity) as GameObject;
            Destroy(skill_2_Effect, 2f);

            getDragon().DecreaseDragonHp(GetPlayer().normalSword_SkillDamage);
        }
        else if (name == EffectName.Skill_3)
        {
            GameObject skill_3_Effect = Instantiate(skill_3_EffectPrefab, effectCreatePos.position, Quaternion.identity) as GameObject;
            Destroy(skill_3_Effect, 2f);

            getDragon().DecreaseDragonHp(GetPlayer().normalSword_SkillDamage);
        }
        else if (name == EffectName.Skill_4)
        {
            GameObject skill_4_Effect = Instantiate(skill_4_EffectPrefab, effectCreatePos.position, Quaternion.identity) as GameObject;
            Destroy(skill_4_Effect, 2f);

            getDragon().DecreaseDragonHp(GetPlayer().normalSword_SkillDamage);
        }
        else if (name == EffectName.Skill_5)
        {
            GameObject skill_5_Effect = Instantiate(skill_5_EffectPrefab, effectCreatePos.position, Quaternion.identity) as GameObject;
            Destroy(skill_5_Effect, 2f);

            getDragon().DecreaseDragonHp(GetPlayer().normalSword_SkillDamage);
        }
        else if(name == EffectName.Skill_6)
        {
            GameObject skill_6_Effect = Instantiate(skill_6_EffectPrefab, effectCreatePos.position, Quaternion.identity) as GameObject;
            Destroy(skill_6_Effect, 2f);

            getDragon().DecreaseDragonHp(GetPlayer().normalSword_SkillDamage);
        }
        else if(name == EffectName.createBoxEffect)
        {
            GameObject createBoxEffect = Instantiate(createBoxEffectPrefab, pos, Quaternion.identity) as GameObject;
            Destroy(createBoxEffect, 2f);

        }
    }   

    

}

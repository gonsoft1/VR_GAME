using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBox : MonoBehaviour {

    public BoxKind boxKind;
    public emBoxNum boxNum;
    public float moveSpeed;

    public Transform playerPosLeft;
    public Transform playerPosRight;
    public Transform PlayerPosCenter;

    public Transform playerPosLeftRND;
    public Transform playerPosRightRND;
    
    Vector3 moveDirection;
    public Transform tr;
    public int belongPatternNum;
    public int directionName;  // 0 왼쪽 , 1 오른쪽 , 2 센터
    public bool isMove = true;
    public bool leftRightTargetBox = false;

    // Use this for initialization
    void Start() {
        tr = GetComponent<Transform>();

        playerPosLeft = GameObject.Find("RefPosLeft").transform;
        playerPosRight = GameObject.Find("RefPosRight").transform;
        PlayerPosCenter = GameObject.Find("RefPosCenter").transform;

        playerPosLeftRND = GameObject.Find("RefPosLeft").transform.Find("PosLeftRND").transform;
        playerPosRightRND = GameObject.Find("RefPosRight").transform.Find("PosRightRND").transform;

        belongPatternNum = getSpawnManager().currentboxPatternNum;
        directionName = getSpawnManager().whatCreatePosLeftOrRight;
        boxKind = getSpawnManager().currentCreateBoxKind;
    }

    // Update is called once per frame
    void Update() {

        if (isMove) Move();

        if(!isMove) TargetBoxMissRotate();
    }

    SpawnManager getSpawnManager()
    {
        GameObject smobj = GameObject.Find("BoxSpawner") as GameObject;
        SpawnManager sm = smobj.GetComponent<SpawnManager>();
        return sm;
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
    
    
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == Tag.Player.ToString())
        {
            if (boxNum == emBoxNum.First)
            {
                getSpawnManager().StartCombo();
            }

            if (boxKind == BoxKind.EnemyAttackBox)
            {
                // 유저 체력 깍이거나 죽음        
                GetPlayer().DecreasePlayerHp();                
            }

            if(boxKind == BoxKind.TargetBox)
            { 
                getSpawnManager().InsertCheckCombo(false);
            }

            getSpawnManager().EffectInst(EffectName.missBoxEffect, tr.position);

            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == Tag.Sword.ToString() && boxKind == BoxKind.TargetBox)
        {
          
            if (boxNum == emBoxNum.First)
            {
                getSpawnManager().StartCombo();
            }
           
            transform.Find("MissCollider").GetComponent<BoxCollider>().enabled = false;                       


            // 용 체력 깍음
            getDragon().DecreaseDragonHp(GetPlayer().CalcAttackDamage());


            // 이펙트 생성
            getSpawnManager().EffectInst(EffectName.hitBoxEffect, tr.position);

            // 효과음 재생
            getSpawnManager().SoundPlay(SoundName.hitBox);            

            getSpawnManager().InsertCheckCombo(true);

            if (boxNum == emBoxNum.Last)
            {
                getSpawnManager().CompareCheckEndCombo(belongPatternNum);
            }

            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == Tag.Shield.ToString() && boxKind == BoxKind.EnemyAttackBox)
        {
            getSpawnManager().EffectInst(EffectName.missBoxEffect, tr.position);
            getSpawnManager().SoundPlay(SoundName.blockBox);
            Destroy(this.gameObject);
        }        
    } 

    void Move()
    {
        if (directionName == 0)
        {
            transform.LookAt(playerPosLeft);

            //if(leftRightTargetBox)
            //{
            //    transform.position = Vector3.MoveTowards(tr.position, playerPosLeftRND.position, moveSpeed * Time.deltaTime);
            //}
            //else
            //{ 
                transform.position = Vector3.MoveTowards(tr.position, playerPosLeft.position, moveSpeed * Time.deltaTime);
            //}
        }
        else if (directionName == 1)
        {
            transform.LookAt(playerPosRight);

            //if(leftRightTargetBox)
            //{
            //    transform.position = Vector3.MoveTowards(tr.position, playerPosRightRND.position, moveSpeed * Time.deltaTime);
            //}
            //else
            //{ 
                transform.position = Vector3.MoveTowards(tr.position, playerPosRight.position, moveSpeed * Time.deltaTime);
            //}
        }
        else
        {
            transform.LookAt(PlayerPosCenter);
            transform.position = Vector3.MoveTowards(tr.position, PlayerPosCenter.position, moveSpeed * Time.deltaTime);
        }
    }

    void TargetBoxMissRotate()
    {
        transform.Rotate(new Vector3(1.5f, 6.5f, 3f));
    }
}



using System.Collections;
using System.Collections.Generic;
using Fix;
using UnityEngine;

public class Test : MonoBehaviour
{
    private LockStep logic;

    public uint tickInterval = 300;
    public uint tickRate = 3;

    public ulong frameCount;

    public ulong tickCount;

    public uint tickCurrentInterval;

    private float mTime;

    public float tickSpeed = 1;

    public Vector2 direction;

    public ulong entityID = 100;

    public LogicEntity entity;

    public SeparateTree mTree;

    public TestEntity TestEntity;
    // Use this for initialization
    void Start () {
		
        logic = new LockStep(tickInterval,tickRate);
        logic.onTick += OnTick;

        Vector3 size = new Vector3(100,0,100);
        mTree = new SeparateTree(SeparateType.QuadTree, Vector3.zero, size, 4);

        mTree.Add(TestEntity);
    }
	
	// Update is called once per frame
	void Update ()
    {
        logic.Update(Time.deltaTime);


        frameCount = logic.mFrameCount;
        tickCount = logic.mTickCount;
        tickCurrentInterval = logic.mTickCurrentInterval;


       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            logic.End();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            logic.Start();

            entity = LogicManager.Instance.AddEntity(entityID);
            entity.position = FixVector2.zero;
            entity.speed = 10;

        }

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        direction.Normalize();

        ServerTick();


    }
    /// <summary>
    /// 模拟服务器推送帧
    /// </summary>
    private void ServerTick()
    {
        mTime += Time.deltaTime * tickSpeed;

        if (mTime * 1000 >= tickInterval * tickRate)
        {
            logic.Next();
            LogicManager.Instance.AddCommand(logic.mFrameCount, entityID, new LogicCommandMove { direction = new FixVector2(direction.x,direction.y) });
            mTime -= (tickInterval * tickRate) / 1000f;
        }
    }

    private void OnTick(ulong frameCount, uint deltaTime)
    {
       // Debug.Log("frameCount:"+frameCount +" deltaTime:"+deltaTime);

        LogicManager.Instance.Update(frameCount,deltaTime);

    }


    void OnDrawGizmos()
    {
        if (mTree != null)
        {
            mTree.DrawTree(Color.white, Color.black, Color.green, 0, 4, true);
        }
    }
}

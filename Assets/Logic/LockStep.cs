using System;

public class LockStep
{
    private bool mRunning = false;
 
    /// <summary>
    /// 帧数(服务器推来的)
    /// </summary>
    public ulong mFrameCount { get; private set; }
    /// <summary>
    /// 当前运行到的帧数
    /// </summary>
    public ulong mCurrentFrameCount {get { return mTickCount / mTickRate; }}
    /// <summary>
    /// Tick次数
    /// </summary>
    public ulong mTickCount { get; private set; }
    /// <summary>
    /// 默认每次Tick间隔时间(毫秒)
    /// </summary>
    public uint mTickInterval { get; private set; }
    /// <summary>
    /// 当前每次Tick间隔时间（毫秒）
    /// </summary>
    public uint mTickCurrentInterval { get; private set; }
    /// <summary>
    /// 每一帧Tick次数
    /// </summary>
    public uint mTickRate { get; private set; }

    /// <summary>
    /// 总共运行时间
    /// </summary>
    public float mTotalTime { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    private float mTickTime { get;  set; }

    public event Action<ulong, uint> onTick;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tickInterval">tick间隔（毫秒）</param>
    /// <param name="tickRate">tick频率（一帧tick多少次）</param>
    public LockStep(uint tickInterval, uint tickRate)
    {
        mTickInterval = tickInterval;
        mTickRate = tickRate;
    }

    /// <summary>
    /// 开始
    /// </summary>
    public void Start()
    {
        mRunning = true;
        mFrameCount = 0;
        mTickCount = 0;
        mTickTime = 0;
        mTotalTime = 0;

        Next();

    }
    /// <summary>
    /// 帧Tick
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        if (mRunning == false && mTickCount == mFrameCount * mTickRate)
        {
            return;
        }

        mTickTime += deltaTime;
        mTotalTime += deltaTime;

        while (mTickTime * 1000 > mTickCurrentInterval)
        {
            mTickTime -= mTickCurrentInterval / 1000f;

            if (mTickCount < mFrameCount * mTickRate)
            {
                if (onTick != null)
                {
                    onTick(mCurrentFrameCount, mTickInterval);
                }

                mTickCount++;
            }
        }
    }

    /// <summary>
    /// 服务器推下一帧
    /// </summary>
    public void Next()
    {
        if (mRunning)
        {
            mFrameCount++;

            ulong count = mFrameCount * mTickRate - mTickCount;
            mTickCurrentInterval = (uint)(mTickInterval * mTickRate / count);
        }
    }
    /// <summary>
    /// 停止
    /// </summary>
    public void End()
    {
        mRunning = false;
    }
}


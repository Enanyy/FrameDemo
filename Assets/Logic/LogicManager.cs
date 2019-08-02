using System;
using System.Collections.Generic;

public class LogicManager
{
    private static LogicManager mInstance;

    public static LogicManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new LogicManager();
            }

            return mInstance;
        }
    }

    public Dictionary<ulong,LogicEntity> entities { get; private set; }

    public LogicCommandQueue commandQueue = new LogicCommandQueue();

    public ulong frameCount = 0;


    public LogicManager()
    {
        entities = new Dictionary<ulong, LogicEntity>();
    }

    public void Update(ulong frameCount, uint deltaTime)
    {
        var it = entities.GetEnumerator();
        while (it.MoveNext())
        {
            if (frameCount > this.frameCount)
            {
                var commands = commandQueue.GetCommands(frameCount, it.Current.Key);
                if (commands != null)
                {
                    it.Current.Value.ExcuteCommand(commands);
                }

                this.frameCount = frameCount;
            }

            it.Current.Value.Update(deltaTime);
        }
    }

    public LogicEntity AddEntity(ulong id)
    {
        var entity = GetEntity(id);
        if (entity == null)
        {
            entity = new LogicEntity(id);
            entities.Add(id,entity);
        }

        return entity;
    }

    public LogicEntity GetEntity(ulong id)
    {
        LogicEntity entity;
        entities.TryGetValue(id, out entity);
        return entity;
    }

    public bool RemoveEntity(ulong id)
    {
        return entities.Remove(id);
    }

    public void AddCommand(ulong frameCount, ulong entity, LogicCommand command)
    {
        commandQueue.AddCommand(frameCount,entity,command);
    }



    
}


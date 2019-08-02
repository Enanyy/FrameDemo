using System;
using System.Collections.Generic;
using Fix;
using UnityEngine;


[Serializable]
public class LogicEntity
{
    public ulong id { get; private set; }
    public FixVector2 position;
    public FixVector2 direction;
   
    public fixed64 speed;

    public GameObject gameObject;

    public LogicEntity(ulong id)
    {
        this.id = id;
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    public void Update(uint deltaTime)
    {
        if (direction != FixVector2.zero)
        {
            fixed64 time = deltaTime / 1000f;
            position += direction *  speed * time;
        }

        float x = (float)position.x;
        float z = (float) position.y;


        gameObject.transform.position = new Vector3(x, 0, z);
    }

    public void ExcuteCommand(LogicCommand command)
    {
        command.Excute(this);
    }

    public void ExcuteCommand(Dictionary<CommandID, LogicCommand> commands)
    {
        if (commands != null)
        {
            var it = commands.GetEnumerator();
            while (it.MoveNext())
            {
                ExcuteCommand(it.Current.Value);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using Fix;
using UnityEngine;

public enum CommandID
{
    Move,
}

public abstract class LogicCommand
{
    public readonly CommandID id;

    public LogicCommand(CommandID id)
    {
        this.id = id;
    }

    public abstract void Excute(LogicEntity entity);
}

public class LogicCommandMove : LogicCommand
{
    public FixVector2 direction;

    public LogicCommandMove() : base(CommandID.Move)
    {

    }

    public override void Excute(LogicEntity entity)
    {
        entity.direction = direction;
    }
}


using System;
using System.Collections.Generic;


public class LogicCommandQueue
{
    public Dictionary<ulong, Dictionary<ulong, Dictionary<CommandID,LogicCommand>>> commands =
        new Dictionary<ulong, Dictionary<ulong, Dictionary<CommandID, LogicCommand>>>();

    public void AddCommand(ulong frameCount, ulong entity, LogicCommand command)
    {
        if (commands.ContainsKey(frameCount) == false)
        {
            commands.Add(frameCount,new Dictionary<ulong, Dictionary<CommandID, LogicCommand>>());
        }

        if (commands[frameCount].ContainsKey(entity) == false)
        {
            commands[frameCount].Add(entity,new Dictionary<CommandID, LogicCommand>());
        }

        if (commands[frameCount][entity].ContainsKey(command.id) == false)
        {
            commands[frameCount][entity].Add(command.id,command);
        }
        else
        {
            commands[frameCount][entity][command.id] = command;
        }
    }

    public Dictionary<CommandID,LogicCommand> GetCommands(ulong frameCount, ulong entity)
    {
        if (commands.ContainsKey(frameCount) && commands[frameCount].ContainsKey(entity))
        {
            return commands[frameCount][entity];
        }

        return null;
    }
}


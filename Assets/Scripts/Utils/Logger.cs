using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    private static ILogger logger = Debug.unityLogger;
    private static string tag = "PedagogyVR";

    public static void Print(object message)
    {
        logger.Log(tag, message);
    }

    public static void Warning(object message)
    {
        logger.LogWarning(tag, message);
    }

    public static void Error(object message)
    {
        logger.LogError(tag, message);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Screen = Base.UI.Screen;

public class RulesetInfo {

    public String InstantiationInfo;
    
    /// <summary>
    /// 無法直接把string轉成泛型，只能用invoke
    /// </summary>
    /// <param name="screen"></param>
    /// <returns></returns>
    public virtual Ruleset CreateInstance(Screen screen)
    {
        Type genericType = Assembly.GetExecutingAssembly().GetType(InstantiationInfo);
        return (Ruleset)typeof(Screen)
            .GetMethod("New")
            .MakeGenericMethod(genericType)
            .Invoke(screen, null);
    }
}

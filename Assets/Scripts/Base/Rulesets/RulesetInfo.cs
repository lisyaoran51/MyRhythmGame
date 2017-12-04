using Base.Rulesets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Screen = Base.UI.Screen;

public class RulesetInfo {

    public int ID;

    public string Name;

    public string InstantiationInfo;

    public RulesetInfo() { }

    public RulesetInfo(string rulesetName) {
        InstantiationInfo = rulesetName;
    }

    /// <summary>
    /// 無法直接把string轉成泛型，只能用invoke
    /// </summary>
    /// <param name="screen"></param>
    /// <returns></returns>
    public virtual Ruleset CreateInstance()
    {
        //Type genericType = Assembly.GetExecutingAssembly().GetType(InstantiationInfo);
        return (Ruleset)Activator.CreateInstance(Type.GetType(InstantiationInfo), this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Base.Sheetmusics;
using UnityEngine;

public class IntangibleScreen : IIntangible {
    private RulesetInfo rulesetInfo;
    public RulesetInfo RulesetInfo {
        protected set { rulesetInfo = value; }
        get { return rulesetInfo; }
    }

    private WorkingSheetmusic workingSheetmusic;
    public WorkingSheetmusic WorkingSheetmusic {
        protected set { workingSheetmusic = value; }
        get { return workingSheetmusic; }
    }


    public IntangibleScreen(RulesetInfo rulesetInfo, WorkingSheetmusic workingSheetmusic) {
        Class = typeof(Screen);
        this.rulesetInfo = rulesetInfo;
        this.workingSheetmusic = workingSheetmusic;
    }

    public Type Class {
        get;  set;
    }


}

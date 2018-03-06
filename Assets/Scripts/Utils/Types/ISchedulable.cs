using Base.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISchedulable{
    ScheduledDelegate Schedule(Action task, double delay);
}

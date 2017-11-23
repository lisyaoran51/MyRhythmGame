using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawable : ChildAddable {
    
}


public enum Axes {

    None = 0,

    X = 1 << 0,
    Y = 1 << 1,

    Both = X | Y
}
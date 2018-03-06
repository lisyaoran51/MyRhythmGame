using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerialPortManager<T>
    where T : class{

    /// <summary>
    /// The backing file used to store the config. Null means no persistent storage.
    /// </summary>
    protected virtual string Filename { get { return @"game.ini"; } }

    private readonly Dictionary<T, IComparable> configStore = new Dictionary<T, IComparable>();

    public SerialPortManager<T> Set(T lookup, int value) {
        configStore[lookup] = value;
        return this;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils.Types {
    public interface INewable {
        T New<T>(object[] param, string name = null) where T : Newable;
    }
}
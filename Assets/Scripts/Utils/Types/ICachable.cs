using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils.Types {
    public interface ICachable {
        void Cache(object o);

        object GetCache(Type type);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils.Types {
    /// <summary>
    /// 在新物件加入父物件時，會將父物件的資料load進來
    /// </summary>
    public interface ILoadable {
        void Load<T>(T instance);
    }
}
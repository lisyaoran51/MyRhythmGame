using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Types {
    public interface IInjectable {
        void Inject(IInjectable instance);
    }
}
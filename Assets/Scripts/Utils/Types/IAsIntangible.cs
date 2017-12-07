using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface IAsIntangible<T>
    where T : IIntangible{
    

    // 轉成沒有monobehavior的物件
    T AsIntangible();
}

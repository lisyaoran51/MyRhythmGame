using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Base.IO.SerialPorts {
    /// <summary>
    /// 提供Serial port manager從程式資訊轉換成傳送訊息的轉換器，toString()用來將物鍵轉換成訊息，
    /// 再由serial port manager把這個訊息傳送到port上。
    /// </summary>
    public interface ISerialPortCall {
        SerialPortCallType Type { get; }
        string ToString();
    }

    /// <summary>
    /// call有兩種形式，system形式是關於遊戲系統的指令，例如遊戲暫停、設定調整等
    /// event則是遊戲中的事件，如產生燈光效果等。
    /// </summary>
    public enum SerialPortCallType {
        System,
        Event
    }
}
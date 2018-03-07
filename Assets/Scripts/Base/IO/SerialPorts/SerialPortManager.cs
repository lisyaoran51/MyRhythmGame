using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

namespace Base.IO.SerialPorts {
    public class SerialPortManager {

        private SerialPort serialPort;
        private bool isOpen {
            get { return serialPort.IsOpen; }
        }

        public bool IsOpen {
            get { return isOpen; }
        }

        public SerialPortManager(String port, int baudRate) {
            serialPort = new SerialPort(port, baudRate);
            serialPort.ReadTimeout = 50;
            if (!serialPort.IsOpen) {
                serialPort.Open();
            }
        }

        public void WriteToArduino(string message) {
            if (isOpen) {
                serialPort.WriteLine(message);
                serialPort.BaseStream.Flush();
            }else {
                throw new InvalidOperationException("Failed to write messages to Arduino: the port is not open.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>打開成功就回傳true否則false</returns>
        public bool Open() {
            if (isOpen) return true;
            serialPort.Open();
            return isOpen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>關閉成功就回傳true否則false</returns>
        public bool Close() {
            if (!isOpen) return true;
            serialPort.Close();
            return !isOpen;
        }

        public void Call(ISerialPortCall call) {
            try {
                WriteToArduino(call.ToString());
            }catch(Exception e) {
                // ???待哺
                throw e;
            }
        }
    }
}

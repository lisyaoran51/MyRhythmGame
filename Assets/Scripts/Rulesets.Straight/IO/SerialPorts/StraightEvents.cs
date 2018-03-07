using Base.IO.SerialPorts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Base.Rulesets.Straight.IO.SerialPorts {
    public class StraightEvent : ISerialPortCall {

        private StraightEventType eventType;

        private Pitch pitch;

        private double visibleTimeRange;

        private double duration;

        public StraightEvent(StraightEventType eventType, Pitch pitch, double visibleTimeRange, double duration = -1) {
            this.eventType = eventType;
            this.pitch = pitch;
            this.visibleTimeRange = visibleTimeRange;
            this.duration = duration;

            if(eventType > StraightEventType.Note && duration == -1) {
                throw new ArgumentException("Failed to construct straight event: " + eventType.ToString() + "needs the duration argument.");
            }
        }

        public override string ToString() {
            string s = "Event " + eventType.ToString() + " " + (int)pitch + " " + string.Format("{0:N2}", visibleTimeRange);
            if(eventType > StraightEventType.Note)
                s += " " + string.Format("{0:N2}", duration);
            Debug.Log(s);
            return s;
        }

        public SerialPortCallType Type {
            get {
                return SerialPortCallType.Event;
            }
        }


    }

    public enum StraightEventType {
        Note,
        HoldNote,
        Batter
    }
}
using System.Collections;
using System.Collections.Generic;
using Base.Sheetmusics.Timing;

namespace Base.Sheetmusics.ControlPoints {
    public class TimingControlPoint : ControlPoint {
        public int Column { get; internal set; }
        public double Time { get; internal set; }
        public double NoteLength { get; internal set; }
        public TimeSignatures TimeSignature { get; internal set; }

        
    }
}
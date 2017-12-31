using System.Collections;
using System.Collections.Generic;
using Base.Sheetmusics.Timing;

namespace Base.Sheetmusics.ControlPoints {
    public class TimingControlPoint : ControlPoint {
        

        public float NoteLength { get; internal set; }

        public TimeSignatures TimeSignature { get; internal set; }

        
    }
}
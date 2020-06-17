// Author: MyName
// Copyright:   Copyright 2020 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace Power.Analyzer
{
    [Display("Current", Group: "Power.Analyzer", Description: "Insert a description here")]
    public class Current : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("SourceCurrentLevelImm", "", "Input Parameters", 2)]
        public double SourceCurrentLevelImm { get; set; } = 1D;

        [DisplayAttribute("SourceCurrentLevelTrig", "", "Input Parameters", 2)]
        public double SourceCurrentLevelTrig { get; set; } = 1D;

        [DisplayAttribute("CurrentLimitState", "", "Input Parameters", 2)]
        public bool CurrentLimitState { get; set; } = true;

        [DisplayAttribute("CurrentLimitPositive", "", "Input Parameters", 2)]
        public double CurrentLimitPositive { get; set; } = 2D;

        [DisplayAttribute("CurrentLimitNegative", "", "Input Parameters", 2)]
        public double CurrentLimitNegative { get; set; } = -2D;

        [DisplayAttribute("CurrentMode", "{ FIX, STEP, LIST, ARB }", "Input Parameters", 2)]
        public string CurrentMode { get; set; } = "FIXed";

        [DisplayAttribute("CurrentProtectionDelayStart", @"SCHange - starts the over-current delay timer at the END of a settings change in voltage, current, or output state; allowing for additional protection delay time.
CCTRans - starts the over-current delay timer by any transition of the output into current limit mode.", "Input Parameters", 2)]
        public string CurrentProtectionDelayStart { get; set; } = "SCHange";

        [DisplayAttribute("CurrentProtectionDelayTime", "Sets the over-current protection delay. ", "Input Parameters", 2)]
        public double CurrentProtectionDelayTime { get; set; } = 0.02D;

        [DisplayAttribute("CurrentProtectionState", "", "Input Parameters", 2)]
        public bool CurrentProtectionState { get; set; } = true;

        [DisplayAttribute("CurrentRange", "", "Input Parameters", 2)]
        public string CurrentRange { get; set; } = "MAX";

        [DisplayAttribute("CurrentSlewrate", "", "Input Parameters", 2)]
        public string CurrentSlewrate { get; set; } = "MAX";

        [DisplayAttribute("OutputRegulationPriority", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public string OutputRegulationPriority { get; set; } = "VOLTage";


        #endregion

        public Current()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:CURRent:LEVel:IMMediate:AMPLitude {0},{1}", SourceCurrentLevelImm, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:LEVel:TRIGgered:AMPLitude {0},{1}", SourceCurrentLevelTrig, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:LIMit:COUPle {0},{1}", CurrentLimitState, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:LIMit:POSitive:IMMediate:AMPLitude {0},{1}", CurrentLimitPositive, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:LIMit:NEGative:IMMediate:AMPLitude {0},{1}", CurrentLimitNegative, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:MODE {0},{1}", CurrentMode, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:PROTection:DELay:STARt {0},{1}", CurrentProtectionDelayStart, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:PROTection:DELay:TIME {0},{1}", CurrentProtectionDelayTime, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:PROTection:STATe {0},{1}", CurrentProtectionState, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:RANGe {0},{1}", CurrentRange, ChanList);
            MyInst.ScpiCommand(":SOURce:CURRent:SLEW:IMMediate {0},{1}", CurrentSlewrate, ChanList);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

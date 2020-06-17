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
    [Display("Voltage", Group: "Power.Analyzer", Description: "Insert a description here")]
    public class Voltage : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("VoltageBW", "( LOW, HIGH1, HIGH2, HIGH3 }", "Input Parameters", 2)]
        public string VoltageBW { get; set; } = "LOW";

        [DisplayAttribute("SourceVoltLevelImm", "", "Input Parameters", 2)]
        public double SourceVoltLevelImm { get; set; } = 1D;

        [DisplayAttribute("VoltLimitNegative", "", "Input Parameters", 2)]
        public string VoltLimitNegative { get; set; } = "MAX";

        [DisplayAttribute("VoltLimitPositive", "", "Input Parameters", 2)]
        public string VoltLimitPositive { get; set; } = "MAXimum";

        [DisplayAttribute("VoltMode", "", "Input Parameters", 2)]
        public string VoltMode { get; set; } = "FIXed";

        [DisplayAttribute("VoltProtectionDelayTime", "60 μs - 5 ms | MIN | MAX", "Input Parameters", 2)]
        public double VoltProtectionDelayTime { get; set; } = 0D;

        [DisplayAttribute("VoltProtectionLevel", "", "Input Parameters", 2)]
        public double VoltProtectionLevel { get; set; } = 60D;

        [DisplayAttribute("VoltRange", "Value, Max, Min", "Input Parameters", 2)]
        public string VoltRange { get; set; } = "MAX";

        [DisplayAttribute("VoltSlewrate", "0 – 9.9E+37|MIN|MAX|INFinity", "Input Parameters", 2)]
        public string VoltSlewrate { get; set; } = "MAXimum";


        #endregion

        public Voltage()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:VOLTage:BWIDth {0},{1}", VoltageBW, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:LEVel:IMMediate:AMPLitude {0},{1}", SourceVoltLevelImm, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:LIMit:NEGative:IMMediate:AMPLitude {0},{1}", VoltLimitNegative, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:LIMit:POSitive:IMMediate:AMPLitude {0},{1}", VoltLimitPositive, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:LIMit:POSitive:IMMediate:AMPLitude {0},{1}", VoltLimitPositive, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:MODE {0},{1}", VoltMode, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:PROTection:DELay:TIME {0},{1}", VoltProtectionDelayTime, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:PROTection:LEVel {0},{1}", VoltProtectionLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:RANGe {0},{1}", VoltRange, ChanList);
            MyInst.ScpiCommand(":SOURce:VOLTage:SLEW:IMMediate {0},{1}", VoltSlewrate, ChanList);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

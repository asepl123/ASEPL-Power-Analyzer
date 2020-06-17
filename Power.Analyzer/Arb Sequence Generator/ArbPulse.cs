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
    [Display("Arbitraty Pulse", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbPulse : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "PULSe";


        [DisplayAttribute("CPulseStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CPulseStartLevel { get; set; } = 1D;

        [DisplayAttribute("CPulseStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CPulseStartTime { get; set; } = 1D;

        [DisplayAttribute("CPulseTopLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CPulseTopLevel { get; set; } = 1D;

        [DisplayAttribute("CPulseTopTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CPulseTopTime { get; set; } = 1D;

        [DisplayAttribute("CPulseEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CPulseEndTime { get; set; } = 1D;



        [DisplayAttribute("VPulseStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VPulseStartLevel { get; set; } = 1D;

        [DisplayAttribute("VPulseStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VPulseStartTime { get; set; } = 1D;

        [DisplayAttribute("VPulseTopLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VPulseTopLevel { get; set; } = 1D;

        [DisplayAttribute("VPulseTopTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VPulseTopTime { get; set; } = 1D;

        [DisplayAttribute("VPulseEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VPulseEndTime { get; set; } = 1D;


        #endregion

        public ArbPulse()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Pulse
            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:STARt:LEVel {0},{1}", CPulseStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:STARt:TIMe {0},{1}", CPulseStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:TOP:LEVel {0},{1}", CPulseTopLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:TOP:TIMe {0},{1}", CPulseTopTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:END:TIMe {0},{1}", CPulseEndTime, ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:STARt:LEVel {0},{1}", VPulseStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:STARt:TIMe {0},{1}", VPulseStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:TOP:LEVel {0},{1}", VPulseTopLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:TOP:TIMe {0},{1}", VPulseTopTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:END:TIMe {0},{1}", VPulseEndTime, ChanList);
            }

            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

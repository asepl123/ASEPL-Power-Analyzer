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
    [Display("Arbitraty Ramp", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbRamp : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "RAMP";



        [DisplayAttribute("CRampStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CRampStartLevel { get; set; } = 1D;

        [DisplayAttribute("CRampStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CRampStartTime { get; set; } = 1D;

        [DisplayAttribute("CRampRtime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CRampRtime { get; set; } = 1D;

        [DisplayAttribute("CRampEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CRampEndLevel { get; set; } = 1D;

        [DisplayAttribute("CRampEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CRampEndTime { get; set; } = 2D;



        [DisplayAttribute("VRampStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VRampStartLevel { get; set; } = 1D;

        [DisplayAttribute("VRampStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VRampStartTime { get; set; } = 1D;

        [DisplayAttribute("VRampRtime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VRampRtime { get; set; } = 1D;

        [DisplayAttribute("VRampEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VRampEndLevel { get; set; } = 1D;

        [DisplayAttribute("VRampEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VRampEndTime { get; set; } = 2D;


        #endregion

        public ArbRamp()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Ramp

            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:STARt:LEVel {0},{1}", CRampStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:STARt:TIMe {0},{1}", CRampStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:RTIMe {0},{1}", CRampRtime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:END:LEVel {0},{1}", CRampEndLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:END:TIMe {0},{1}", CRampEndTime, ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:RAMP:STARt:LEVel {0},{1}", VRampStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:RAMP:STARt:TIMe {0},{1}", VRampStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:RAMP:RTIMe {0},{1}", VRampRtime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:RAMP:END:LEVel {0},{1}", VRampEndLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:RAMP:END:TIMe {0},{1}", VRampEndTime, ChanList);
            }

            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

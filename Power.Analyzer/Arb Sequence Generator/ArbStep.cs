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
    [Display("Arbitraty Step", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbStep : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "STEP";



        [DisplayAttribute("CStepStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStepStartLevel { get; set; } = 1D;

        [DisplayAttribute("CStepStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStepStartTime { get; set; } = 1D;

        [DisplayAttribute("CStepEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStepEndLevel { get; set; } = 3D;



        [DisplayAttribute("VStepStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStepStartLevel { get; set; } = 1D;

        [DisplayAttribute("VStepStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStepStartTime { get; set; } = 1D;

        [DisplayAttribute("VStepEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStepEndLevel { get; set; } = 3D;

        #endregion

        public ArbStep()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Step
            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STEP:STARt:LEVel {0},{1}", CStepStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STEP:STARt:TIMe {0},{1}", CStepStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STEP:END:LEVel {0},{1}", CStepEndLevel, ChanList);
            }
            else if(ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STEP:STARt:LEVel {0},{1}", VStepStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STEP:STARt:TIMe {0},{1}", VStepStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STEP:END:LEVel {0},{1}", VStepEndLevel, ChanList);
            }
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

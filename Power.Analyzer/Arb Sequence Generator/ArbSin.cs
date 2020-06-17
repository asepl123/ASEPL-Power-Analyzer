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
    [Display("Arbitraty Sin", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbSin : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "SINusoid";


        [DisplayAttribute("CSinAmplitude", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CSinAmplitude { get; set; } = 1D;

        [DisplayAttribute("CSinFreq", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CSinFreq { get; set; } = 50D;

        [DisplayAttribute("CSinOffset", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CSinOffset { get; set; } = 0D;


        [DisplayAttribute("VSinAmplitude", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VSinAmplitude { get; set; } = 1D;

        [DisplayAttribute("VSinFreq", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VSinFreq { get; set; } = 50D;

        [DisplayAttribute("VSinOffset", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VSinOffset { get; set; } = 0D;

        #endregion

        public ArbSin()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Sin
            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:SINusoid:AMPLitude {0},{1}", CSinAmplitude, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:SINusoid:FREQuency {0},{1}", CSinFreq, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:SINusoid:OFFSet {0},{1}", CSinOffset, ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:SINusoid:AMPLitude {0},{1}", VSinAmplitude, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:SINusoid:FREQuency {0},{1}", VSinFreq, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:SINusoid:OFFSet {0},{1}", VSinOffset, ChanList);
            }

            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

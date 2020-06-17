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
    [Display("Arbitraty Exponential", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbExponential : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "EXPonential";


        [DisplayAttribute("CExpoStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent)]
        public double CExpoStartLevel { get; set; } = 0D;

        [DisplayAttribute("CExpoStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent)]
        public double CExpoStartTime { get; set; } = 0D;

        [DisplayAttribute("CExpoEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent)]
        public double CExpoEndLevel { get; set; } = 0D;

        [DisplayAttribute("CExpoTimeConst", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent)]
        public double CExpoTimeConst { get; set; } = 0.001D;

        [DisplayAttribute("CExpoTime", " – 262.144 | MIN | MAX", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent)]
        public double CExpoTime { get; set; } = 1D;


        [DisplayAttribute("VExpoStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage)]
        public double VExpoStartLevel { get; set; } = 0D;

        [DisplayAttribute("VExpoStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage)]
        public double VExpoStartTime { get; set; } = 0D;

        [DisplayAttribute("VExpoEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage)]
        public double VExpoEndLevel { get; set; } = 0D;

        [DisplayAttribute("VExpoTimeConst", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage)]
        public double VExpoTimeConst { get; set; } = 0.001D;

        [DisplayAttribute("VExpoTime", " – 262.144 | MIN | MAX", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage)]
        public double VExpoTime { get; set; } = 1D;


        #endregion

        public ArbExponential()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Exponential
            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:STARt:LEVel {0},{1}", CExpoStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:STARt:TIMe {0},{1}", CExpoStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:END:LEVel {0},{1}", CExpoEndLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:TIMe {0},{1}", CExpoTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:TCONstant {0},{1}", CExpoTimeConst, ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:EXPonential:STARt:LEVel {0},{1}", VExpoStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:EXPonential:STARt:TIMe {0},{1}", VExpoStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:EXPonential:END:LEVel {0},{1}", VExpoEndLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:EXPonential:TIMe {0},{1}", VExpoTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:EXPonential:TCONstant {0},{1}", VExpoTimeConst, ChanList);
            }

            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

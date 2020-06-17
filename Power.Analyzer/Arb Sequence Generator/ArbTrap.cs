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
    [Display("Arbitraty Trapezoidal", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbTrap : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "TRAPezoid";



        [DisplayAttribute("CTrapStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapStartLevel { get; set; } = 1D;

        [DisplayAttribute("CTrapStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapStartTime { get; set; } = 1D;

        [DisplayAttribute("CTrapRiseTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapRiseTime { get; set; } = 1D;

        [DisplayAttribute("CTrapTopLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapTopLevel { get; set; } = 3D;

        [DisplayAttribute("CTrapTopTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapTopTime { get; set; } = 1D;

        [DisplayAttribute("CTrapRallTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapRallTime { get; set; } = 1D;

        [DisplayAttribute("CTrapEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CTrapEndTime { get; set; } = 1D;



        [DisplayAttribute("VTrapStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapStartLevel { get; set; } = 1D;

        [DisplayAttribute("VTrapStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapStartTime { get; set; } = 1D;

        [DisplayAttribute("VTrapRiseTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapRiseTime { get; set; } = 1D;

        [DisplayAttribute("VTrapTopLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapTopLevel { get; set; } = 3D;

        [DisplayAttribute("VTrapTopTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapTopTime { get; set; } = 1D;

        [DisplayAttribute("VTrapRallTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapRallTime { get; set; } = 1D;

        [DisplayAttribute("VTrapEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VTrapEndTime { get; set; } = 1D;

        #endregion

        public ArbTrap()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Trapezoidal
            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:STARt:LEVel {0},{1}", CTrapStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:STARt:TIMe {0},{1}", CTrapStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:RTIMe {0},{1}", CTrapRiseTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:TOP:LEVel {0},{1}", CTrapTopLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:TOP:TIMe {0},{1}", CTrapTopTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:FTIMe {0},{1}", CTrapRallTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:END:TIMe {0},{1}", CTrapEndTime, ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:STARt:LEVel {0},{1}", VTrapStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:STARt:TIMe {0},{1}", VTrapStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:RTIMe {0},{1}", VTrapRiseTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:TOP:LEVel {0},{1}", VTrapTopLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:TOP:TIMe {0},{1}", VTrapTopTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:FTIMe {0},{1}", VTrapRallTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:TRAPezoid:END:TIMe {0},{1}", VTrapEndTime, ChanList);
            }
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

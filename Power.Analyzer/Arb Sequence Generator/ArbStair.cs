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
    [Display("Arbitraty Stair", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbStair : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "STAircase";



        [DisplayAttribute("CStairStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStairStartLevel { get; set; } = 0D;

        [DisplayAttribute("CStairStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStairStartTime { get; set; } = 1D;

        [DisplayAttribute("CStairSteps", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public int CStairSteps { get; set; } = 10;

        [DisplayAttribute("CStairEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStairEndLevel { get; set; } = 10D;

        [DisplayAttribute("CStairEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStairEndTime { get; set; } = 1D;

        [DisplayAttribute("CStairTotalTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CStairTotalTime { get; set; } = 3D;



        [DisplayAttribute("VStairStartLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStairStartLevel { get; set; } = 0D;

        [DisplayAttribute("VStairStartTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStairStartTime { get; set; } = 1D;

        [DisplayAttribute("VStairSteps", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public int VStairSteps { get; set; } = 10;

        [DisplayAttribute("VStairEndLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStairEndLevel { get; set; } = 10D;

        [DisplayAttribute("VStairEndTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStairEndTime { get; set; } = 1D;

        [DisplayAttribute("VStairTotalTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VStairTotalTime { get; set; } = 3D;

        #endregion

        public ArbStair()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Stair
            if (ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:STARt:LEVel {0},{1}", CStairStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:STARt:TIMe {0},{1}", CStairStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:END:LEVel {0},{1}", CStairEndLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:END:TIMe {0},{1}", CStairEndTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:NSTeps {0},{1}", CStairSteps, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:TIMe {0},{1}", CStairTotalTime, ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STAircase:STARt:LEVel {0},{1}", VStairStartLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STAircase:STARt:TIMe {0},{1}", VStairStartTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STAircase:END:LEVel {0},{1}", VStairEndLevel, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STAircase:END:TIMe {0},{1}", VStairEndTime, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STAircase:NSTeps {0},{1}", VStairSteps, ChanList);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:STAircase:TIMe {0},{1}", VStairTotalTime, ChanList);
            }
        }
    }
}

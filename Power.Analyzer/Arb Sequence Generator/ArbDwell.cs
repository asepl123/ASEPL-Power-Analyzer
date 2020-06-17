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
    public enum EArbType { CURRent, VOLTage }

    [Display("Arbitraty Dwell", Groups: new[] { "Power.Analyzer", "Arbitrary Function" }, Description: "Insert a description here")]
    public class ArbDwell : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public EArbType ArbType { get; set; } = EArbType.VOLTage;

        public string ArbFunction = "CDWel";


        [DisplayAttribute("CDwellTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double CDwellTime { get; set; } = 0.001D;

        [DisplayAttribute("CDwellLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.CURRent, HideIfDisabled = true)]
        public double[] CDwellLevel { get; set; } = new double[] {0D};


        [DisplayAttribute("VDwellTime", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double VDwellTime { get; set; } = 0.001D;

        [DisplayAttribute("VDwellLevel", "", "Input Parameters", 2)]
        [EnabledIf("ArbType", EArbType.VOLTage, HideIfDisabled = true)]
        public double[] VDwellLevel { get; set; } = new double[] { 0D };

        
        private Int32[] CDwellPoints;

        private Int32[] VDwellPoints;

        #endregion

        public ArbDwell()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:TYPE {0},{1}", ArbType, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:FUNCtion:SHAPe {0},{1}", ArbFunction, ChanList);

            // Dwell
            if(ArbType == EArbType.CURRent)
            {
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:CDWell:DWELl {0},{1}", CDwellTime, ChanList);
                MyInst.ScpiCommand(":FORMat:DATA ASC");
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:CDWell:LEVel {0},{1}", CDwellLevel, ChanList);
                CDwellPoints = MyInst.ScpiQuery<System.Int32[]>(Scpi.Format(":SOURce:ARB:CURRent:CDWell:POINts? {0}", ChanList), true);
                MyInst.ScpiCommand(":SOURce:ARB:CURRent:CONVert {0}", ChanList);
            }
            else if (ArbType == EArbType.VOLTage)
            {
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:CDWell:DWELl {0},{1}", VDwellTime, ChanList);
                MyInst.ScpiCommand(":FORMat:DATA ASC");
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:CDWell:LEVel {0},{1}", VDwellLevel, ChanList);
                VDwellPoints = MyInst.ScpiQuery<System.Int32[]>(Scpi.Format(":SOURce:ARB:VOLTage:CDWell:POINts? {0}", ChanList), true);
                MyInst.ScpiCommand(":SOURce:ARB:VOLTage:CONVert {0}", ChanList);
            }


            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

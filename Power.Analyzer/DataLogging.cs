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
    [Display("DataLogging", Group: "Power.Analyzer", Description: "Insert a description here")]
    public class DataLogging : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }


        [DisplayAttribute("CRangeAuto", "", "Input Parameters", 2)]
        public bool CRangeAuto { get; set; } = true;

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("CRangeUpper", "", "Input Parameters", 2)]
        public string CRangeUpper { get; set; } = "MAX";

        [DisplayAttribute("VRangeAuto", "", "Input Parameters", 2)]
        public bool VRangeAuto { get; set; } = true;

        [DisplayAttribute("VRangeUpper", "", "Input Parameters", 2)]
        public string VRangeUpper { get; set; } = "MAX";

        [DisplayAttribute("CFunction", "", "Input Parameters", 2)]
        public bool CFunction { get; set; } = false;

        [DisplayAttribute("VFunction", "", "Input Parameters", 2)]
        public bool VFunction { get; set; } = false;

        [DisplayAttribute("MinMax", "", "Input Parameters", 2)]
        public bool MinMax { get; set; } = false;

        [DisplayAttribute("TrigOffset", "1-100", "Input Parameters", 2)]
        public int TrigOffset { get; set; } = 0;

        [DisplayAttribute("Period", "", "Input Parameters", 2)]
        public double Period { get; set; } = 0.1D;

        [DisplayAttribute("Time", "", "Input Parameters", 2)]
        public int Time { get; set; } = 30;


        #endregion

        public DataLogging()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SENSe:DLOG:FUNCtion:CURRent {0},{1}", CFunction, ChanList);
            MyInst.ScpiCommand(":SENSe:DLOG:FUNCtion:VOLTage {0},{1}", VFunction, ChanList);
            MyInst.ScpiCommand(":SENSe:DLOG:FUNCtion:MINMax {0}", MinMax);
            MyInst.ScpiCommand(":SENSe:DLOG:OFFSet {0}", TrigOffset);
            MyInst.ScpiCommand(":SENSe:DLOG:PERiod {0}", Period);
            MyInst.ScpiCommand(":SENSe:DLOG:TIME {0}", Time);
            MyInst.ScpiCommand(":SENSe:DLOG:CURRent:DC:RANGe:AUTO {0},{1}", CRangeAuto, ChanList);
            MyInst.ScpiCommand(":SENSe:DLOG:CURRent:DC:RANGe:UPPer {0},{1}", CRangeUpper, ChanList);
            MyInst.ScpiCommand(":SENSe:DLOG:VOLTage:DC:RANGe:AUTO {0},{1}", VRangeAuto, ChanList);
            MyInst.ScpiCommand(":SENSe:DLOG:VOLTage:DC:RANGe:UPPer {0},{1}", VRangeUpper, ChanList);

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

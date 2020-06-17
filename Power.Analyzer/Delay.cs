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
    [Display("Delay", Group: "Power.Analyzer", Description: "Insert a description here")]
    public class Delay : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3";

        [DisplayAttribute("FallDelay", "", "Input Parameters", 2)]
        public double FallDelay { get; set; } = 0D;

        [DisplayAttribute("RiseDelay", "", "Input Parameters", 2)]
        public double RiseDelay { get; set; } = 0D;


        #endregion

        public Delay()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":OUTPut:STATe:DELay:FALL {0},{1}", FallDelay, ChanList);
            MyInst.ScpiCommand(":OUTPut:STATe:DELay:RISE {0},{1}", RiseDelay, ChanList);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

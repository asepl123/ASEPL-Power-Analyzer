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
    [Display("Memory", Group: "Power.Analyzer", Description: "Insert a description here")]
    public class Memory : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }


        [DisplayAttribute("ExportDLog", "", "Input Parameters", 2)]
        public string ExportDLog { get; set; } = "file.csv";

        [DisplayAttribute("LoadArbSeq", "", "Input Parameters", 2)]
        public string LoadArbSeq { get; set; } = "file.seq";

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("StoreArbSeq", "", "Input Parameters", 2)]
        public string StoreArbSeq { get; set; } = "file.seq";

        [DisplayAttribute("ImageType", "{ BMP, GIF, PNG }", "Input Parameters", 2)]
        public string ImageType { get; set; } = "PNG";

        #endregion

        public Memory()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":MMEMory:EXPort:DLOG {0}", ExportDLog);
            MyInst.ScpiCommand(":MMEMory:LOAD:ARB:SEQuence {0},{1}", LoadArbSeq, ChanList);
            MyInst.ScpiCommand(":MMEMory:STORe:ARB:SEQuence {0},{1}", StoreArbSeq, ChanList);
            MyInst.ScpiCommand(":HCOPy:SDUMp:DATA:FORMat {0}", ImageType);

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}

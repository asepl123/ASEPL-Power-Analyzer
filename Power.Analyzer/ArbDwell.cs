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
    [Display("MyTestStep1", Group: "Power.Analyzer", Description: "Insert a description here")]
    public class ArbDwell : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N6705C MyInst { get; set; }

        [DisplayAttribute("ChanList", "", "Input Parameters", 2)]
        public string ChanList { get; set; } = "1,2,3,4";

        [DisplayAttribute("ArbType", "{ CURRent, VOLTage }", "Input Parameters", 2)]
        public string ArbType { get; set; } = "VOLTage";

        [DisplayAttribute("ArbFunction", "{ STEP, RAMP, STAircase, SINusoid, PULSe, TRAPezoid, EXPonential, UDEFined, CDWel" +
            "l, SEQuence, NONE }", "Input Parameters", 2)]
        public string ArbFunction { get; set; } = "NONE";

        [DisplayAttribute("CDwellTime", "", "Input Parameters", 2)]
        public double CDwellTime { get; set; } = 0.001D;

        [DisplayAttribute("CDwellLevel", "", "Input Parameters", 2)]
        public double[] CDwellLevel { get; set; } = new double[] {
                0D};

        [DisplayAttribute("CExpoStartLevel", "", "Input Parameters", 2)]
        public double CExpoStartLevel { get; set; } = 0D;

        [DisplayAttribute("CExpoStartTime", "", "Input Parameters", 2)]
        public double CExpoStartTime { get; set; } = 0D;

        [DisplayAttribute("CExpoEndLevel", "", "Input Parameters", 2)]
        public double CExpoEndLevel { get; set; } = 0D;

        [DisplayAttribute("CExpoTimeConst", "", "Input Parameters", 2)]
        public double CExpoTimeConst { get; set; } = 0.001D;

        [DisplayAttribute("CExpoTime", " – 262.144 | MIN | MAX", "Input Parameters", 2)]
        public double CExpoTime { get; set; } = 1D;

        [DisplayAttribute("CPulseStartLevel", "", "Input Parameters", 2)]
        public double CPulseStartLevel { get; set; } = 1D;

        [DisplayAttribute("CPulseStartTime", "", "Input Parameters", 2)]
        public double CPulseStartTime { get; set; } = 1D;

        [DisplayAttribute("CPulseTopLevel", "", "Input Parameters", 2)]
        public double CPulseTopLevel { get; set; } = 1D;

        [DisplayAttribute("CPulseTopTime", "", "Input Parameters", 2)]
        public double CPulseTopTime { get; set; } = 1D;

        [DisplayAttribute("CPulseEndTime", "", "Input Parameters", 2)]
        public double CPulseEndTime { get; set; } = 1D;

        [DisplayAttribute("CRampStartLevel", "", "Input Parameters", 2)]
        public double CRampStartLevel { get; set; } = 1D;

        [DisplayAttribute("CRampStartTime", "", "Input Parameters", 2)]
        public double CRampStartTime { get; set; } = 1D;

        [DisplayAttribute("CRampRtime", "", "Input Parameters", 2)]
        public double CRampRtime { get; set; } = 1D;

        [DisplayAttribute("CRampEndLevel", "", "Input Parameters", 2)]
        public double CRampEndLevel { get; set; } = 1D;

        [DisplayAttribute("CRampEndTime", "", "Input Parameters", 2)]
        public double CRampEndTime { get; set; } = 2D;

        [DisplayAttribute("CSinAmplitude", "", "Input Parameters", 2)]
        public double CSinAmplitude { get; set; } = 1D;

        [DisplayAttribute("CSinFreq", "", "Input Parameters", 2)]
        public double CSinFreq { get; set; } = 50D;

        [DisplayAttribute("CSinOffset", "", "Input Parameters", 2)]
        public double CSinOffset { get; set; } = 0D;

        [DisplayAttribute("CStairStartLevel", "", "Input Parameters", 2)]
        public double CStairStartLevel { get; set; } = 0D;

        [DisplayAttribute("CStairStartTime", "", "Input Parameters", 2)]
        public double CStairStartTime { get; set; } = 1D;

        [DisplayAttribute("CStairSteps", "", "Input Parameters", 2)]
        public int CStairSteps { get; set; } = 10;

        [DisplayAttribute("CStairEndLevel", "", "Input Parameters", 2)]
        public double CStairEndLevel { get; set; } = 10D;

        [DisplayAttribute("CStairEndTime", "", "Input Parameters", 2)]
        public double CStairEndTime { get; set; } = 1D;

        [DisplayAttribute("CStairTotalTime", "", "Input Parameters", 2)]
        public double CStairTotalTime { get; set; } = 3D;

        [DisplayAttribute("CStepStartLevel", "", "Input Parameters", 2)]
        public double CStepStartLevel { get; set; } = 1D;

        [DisplayAttribute("CStepStartTime", "", "Input Parameters", 2)]
        public double CStepStartTime { get; set; } = 1D;

        [DisplayAttribute("CStepEndLevel", "", "Input Parameters", 2)]
        public double CStepEndLevel { get; set; } = 3D;

        [DisplayAttribute("CTrapStartLevel", "", "Input Parameters", 2)]
        public double CTrapStartLevel { get; set; } = 1D;

        [DisplayAttribute("CTrapStartTime", "", "Input Parameters", 2)]
        public double CTrapStartTime { get; set; } = 1D;

        [DisplayAttribute("CTrapRiseTime", "", "Input Parameters", 2)]
        public double CTrapRiseTime { get; set; } = 1D;

        [DisplayAttribute("CTrapTopLevel", "", "Input Parameters", 2)]
        public double CTrapTopLevel { get; set; } = 3D;

        [DisplayAttribute("CTrapTopTime", "", "Input Parameters", 2)]
        public double CTrapTopTime { get; set; } = 1D;

        [DisplayAttribute("CTrapRallTime", "", "Input Parameters", 2)]
        public double CTrapRallTime { get; set; } = 1D;

        [DisplayAttribute("CTrapEndTime", "", "Input Parameters", 2)]
        public double CTrapEndTime { get; set; } = 1D;

        [DisplayAttribute("VDwellTime", "", "Input Parameters", 2)]
        public double VDwellTime { get; set; } = 0.001D;

        private Int32[] CDwellPoints;


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

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:CDWell:DWELl {0},{1}", CDwellTime, ChanList);
            MyInst.ScpiCommand(":FORMat:DATA ASC");
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:CDWell:LEVel {0},{1}", CDwellLevel, ChanList);
            CDwellPoints = MyInst.ScpiQuery<System.Int32[]>(Scpi.Format(":SOURce:ARB:CURRent:CDWell:POINts? {0}", ChanList), true);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:CONVert {0}", ChanList);

            // Exponential

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:STARt:LEVel {0},{1}", CExpoStartLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:STARt:TIMe {0},{1}", CExpoStartTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:END:LEVel {0},{1}", CExpoEndLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:TIMe {0},{1}", CExpoTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:EXPonential:TCONstant {0},{1}", CExpoTimeConst, ChanList);

            // Pulse

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:STARt:LEVel {0},{1}", CPulseStartLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:STARt:TIMe {0},{1}", CPulseStartTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:TOP:LEVel {0},{1}", CPulseTopLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:TOP:TIMe {0},{1}", CPulseTopTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:PULSe:END:TIMe {0},{1}", CPulseEndTime, ChanList);

            // Ramp

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:STARt:LEVel {0},{1}", CRampStartLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:STARt:TIMe {0},{1}", CRampStartTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:RTIMe {0},{1}", CRampRtime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:END:LEVel {0},{1}", CRampEndLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:RAMP:END:TIMe {0},{1}", CRampEndTime, ChanList);

            // Sin

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:SINusoid:AMPLitude {0},{1}", CSinAmplitude, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:SINusoid:FREQuency {0},{1}", CSinFreq, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:SINusoid:OFFSet {0},{1}", CSinOffset, ChanList);

            // Stair

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:STARt:LEVel {0},{1}", CStairStartLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:STARt:TIMe {0},{1}", CStairStartTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:END:LEVel {0},{1}", CStairEndLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:END:TIMe {0},{1}", CStairEndTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:NSTeps {0},{1}", CStairSteps, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STAircase:TIMe {0},{1}", CStairTotalTime, ChanList);

            // Step

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STEP:STARt:LEVel {0},{1}", CStepStartLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STEP:STARt:TIMe {0},{1}", CStepStartTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:STEP:END:LEVel {0},{1}", CStepEndLevel, ChanList);

            // Trapezoidal

            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:STARt:LEVel {0},{1}", CTrapStartLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:STARt:TIMe {0},{1}", CTrapStartTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:RTIMe {0},{1}", CTrapRiseTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:TOP:LEVel {0},{1}", CTrapTopLevel, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:TOP:TIMe {0},{1}", CTrapTopTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:FTIMe {0},{1}", CTrapRallTime, ChanList);
            MyInst.ScpiCommand(":SOURce:ARB:CURRent:TRAPezoid:END:TIMe {0},{1}", CTrapEndTime, ChanList);


            MyInst.ScpiCommand(":SOURce:ARB:VOLTage:CDWell:DWELl {0},{1}", VDwellTime, ChanList);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
